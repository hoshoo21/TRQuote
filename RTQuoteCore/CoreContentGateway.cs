using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net;
using TRQuoteCore.QuoteStructure;
using TRQuoteCore.Decode;
using ThomsonReuters.RFA.Common;
using ThomsonReuters.RFA.Config;
using ThomsonReuters.RFA.Data;
using ThomsonReuters.RFA.Logger;
using ThomsonReuters.RFA.Message;
using ThomsonReuters.RFA.SessionLayer;
using ThomsonReuters.RFA.RDM;

namespace TRQuoteCore.Core
{
    public enum ExchangeFieldId { 
        TradeExchangeId =44,
        AskExchangeId = 3297,
        BidExchangeId = 3298,
        PrimaryExchangeId = 1709
        
    }
    class CoreContentGateway :Client
    {
        bool isLevel2 = false;
        public static int tradeCount;
        public static int bboQuoteCount;
        public static int quoteCount;
        static int totalMsg;
        List<MarketMakerData> objMMData = new List<MarketMakerData>();
        static Thread monitorThread;
        private IQuoteProcessor callback;
        private ConfigDatabase appCfgDataBase = null;
        private StagingConfigDatabase appStgCfgDataBase = null;
        Int64 DictHandle1, DictHandle2;
        private ConfigDatabase cfgDataBase = null;
        private StagingConfigDatabase stgCfgDataBase = null;
        private ConfigurableVariables confgVariables = new ConfigurableVariables();
        private Session session = null;
        private OMMConsumer ommConsumer = null;
        private EventQueue eventQueue = null;
        private ApplicationLogger appLogger = null;
        private AppLoggerMonitor appLoggerMonitor = null;
        private long loggerHandle = 0;
        private long ommConnIntSpecHandle = 0;
        private long ommErrIntSpecHandle = 0;
        private long loginHandle = 0;
        private RDMFidDef currentFidDef = null;
        private bool usingDictRef = false;
        private RDMFieldDictionary rdmFieldDict = null;
        private bool dictionaryLoaded = false;
        private Dictionary<string, ItemInfo> itemList = new Dictionary<string, ItemInfo>();
        private Dictionary<string, CompositeData> Snapshotlist = new Dictionary<string, CompositeData>();
        private TRQuoteCore.Decode.Decoder decoder = null;
        private sealed class ItemInfo
        {
            public long ItemHandle;
            public RFA_String ItemName;
            public string OriginalSymbol = string.Empty;
            public bool Closed;
            public bool isoption = false;
        };

        private class ConfigurableVariables
        {
            public RFA_String RDMFieldDictFilePath = new RFA_String("./RDMFieldDictionary");
            public RFA_String EnumTypeDefFilePath = new RFA_String("./enumtype.def");
            public int RunTimeInSeconds = 0;
            public RFA_String ServiceName = new RFA_String("");
            public RFA_String SessionName = new RFA_String("Session1");
            public RFA_String AppId = new RFA_String("256");
            public RFA_String Position = new RFA_String();
            public RFA_String UserName = new RFA_String("user");
            public RFA_String ItemName = new RFA_String("TRI.N");
            public List<RFA_String> ItemNameViewFIDs = null;
            public List<RFA_String> BatchItemList1 = null;
            public List<RFA_String> BatchItemList2 = null;
            public List<RFA_String> BatchItemList1ViewFIDs = null;
            public List<RFA_String> BatchItemList2ViewFIDs = null;
            public bool SendAttribInfo = false;
            public List<RFA_String> BatchReissueList = null;
            public List<RFA_String> BatchCloseList = null;
            public long BatchReissueTime = 20;
            public long BatchCloseTime = 30;
        };
        public bool LoadDictionaryFromFile(RFA_String rdm_field_dict_path, RFA_String enumtype_def_path)
        {
            if (dictionaryLoaded)
            {
                return true;
            }

            try
            {
                rdmFieldDict = RDMFieldDictionary.Create();
                rdmFieldDict.ReadRDMFieldDictionary(rdm_field_dict_path);
                rdmFieldDict.ReadRDMEnumTypeDef(enumtype_def_path);
                rdmFieldDict.DictId = 1;

                dictionaryLoaded = true;


            }
            catch (InvalidUsageException)
            {
            }

            return dictionaryLoaded;
        }

        public bool InitConfig()
        {
            bool result = false;
            cfgDataBase = ConfigDatabase.Acquire(new RFA_String("RFA"));
            if (cfgDataBase == null)
            {
                return false;
            }
            stgCfgDataBase = StagingConfigDatabase.Create();
            if (stgCfgDataBase == null)
            {
                return false;
            }
            RFA_String exampleRfaCfgFilePath = new RFA_String("./BasicConfig.cfg");

            result = stgCfgDataBase.Load(ConfigRepositoryTypeEnum.flatFile, exampleRfaCfgFilePath);

            if (!result)
            {
                return result;
            }
            result = cfgDataBase.Merge(stgCfgDataBase);
            if (!result)
            {
                return result;
            }
            appCfgDataBase = ConfigDatabase.Acquire(new RFA_String("WatchlistConfig"));
            if (appCfgDataBase == null)
            {
                return false;
            }
            appStgCfgDataBase = StagingConfigDatabase.Create();
            if (appStgCfgDataBase == null)
            {
                return false;
            }
            RFA_String exampleAppCfgFilePath = new RFA_String("./WatchlistConfig.cfg");
            result = appStgCfgDataBase.Load(ConfigRepositoryTypeEnum.flatFile, exampleAppCfgFilePath);
            if (!result)
            {
                return result;
            }
            result = appCfgDataBase.Merge(appStgCfgDataBase);
            return result;

        }
        public static RFA_String GetDefPosition()
        {
            IPHostEntry hostEntry = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress[] addresses = hostEntry.AddressList;

            foreach (var item in addresses)
            {
                if (item.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    return new RFA_String(item.ToString() + "/net");
                }
            }

            return new RFA_String("127.0.0.1/net");
        }
        public bool InitClient() {
            InitConfig();
            bool result = false;
            result = Context.Initialize();
            ReadConfig(appCfgDataBase.ConfigTree);
            eventQueue = EventQueue.Create(new RFA_String("myEventQueue"));
            if (eventQueue == null)
            {
                return false;
            }

            try
            {
                session = Session.Acquire(new RFA_String("Session1"));

            }
            catch (InvalidConfigurationException ex)
            {
                string temp = ex.Message;
            }
            catch (InvalidUsageException ex) { 
                string tem = ex.Message;
            }
            if (session == null)
            {
                return false;
            }
            ommConsumer = session.CreateOMMConsumer(new RFA_String("TRCore"));
            if (ommConsumer == null)
            {
                return false;
            }
            OMMConnectionIntSpec connectionIntSpec = new OMMConnectionIntSpec();

            ommConnIntSpecHandle = ommConsumer.RegisterClient(null, connectionIntSpec, this, null);
            if (ommConnIntSpecHandle == 0)
            {
                return false;
            }
            if (decoder == null)
            {
                decoder = new TRQuoteCore.Decode.Decoder();
            }
            decoder.LoadDictionaryFromFile(confgVariables.RDMFieldDictFilePath, confgVariables.EnumTypeDefFilePath);
            SendLoginRequest();
            return result;
           
        }

        public string getPrimaryExchange(ushort ExchangeValue) {
            return decoder.GetExchangeCode(short.Parse("1709") , ExchangeValue);
        }
        public string getTradingExchange(ushort ExchangeValue) {
            return decoder.GetExchangeCode(short.Parse("44"), ExchangeValue);
        }

        private void ReadConfig(ConfigTree root)
        {
            RFA_String rf = new RFA_String();



            RFA_String defStr = new RFA_String("");
            confgVariables.Position = GetDefPosition();
            confgVariables.RDMFieldDictFilePath = root.GetChildAsString(new RFA_String("Default\\fieldDictionaryFilename"), confgVariables.RDMFieldDictFilePath);
            confgVariables.EnumTypeDefFilePath = root.GetChildAsString(new RFA_String("Default\\enumDictionaryFilename"), confgVariables.EnumTypeDefFilePath);
            confgVariables.AppId = root.GetChildAsString(new RFA_String("Default\\appId"), confgVariables.AppId);
            confgVariables.Position = root.GetChildAsString(new RFA_String("Default\\position"), confgVariables.Position);
            confgVariables.UserName = root.GetChildAsString(new RFA_String("Default\\userName"), confgVariables.UserName);
            confgVariables.ServiceName = root.GetChildAsString(new RFA_String("Default\\service"), confgVariables.ServiceName);
            confgVariables.SessionName = root.GetChildAsString(new RFA_String("Default\\session"), confgVariables.SessionName);           

        }
       // protected ConnectParameters _connection;
        public CoreContentGateway(IQuoteProcessor callback)
        {
            this.callback = callback;
            InitClient();
        }
        internal delegate void ReconnectDelegate();

        public delegate void RequestInvoker(string Symbol);

        Dictionary<string, string> pinksheetSymbols = new Dictionary<string, string>();
       // private Dictionary<ushort, QuoteData> quoteList = new Dictionary<ushort, QuoteData>();
    
        public void RequestQmonData(string Symbol)
        {
            if (Snapshotlist.ContainsKey(Symbol)){
                callback.OnCompositeSnapshot(Snapshotlist[Symbol]);
            }
            new RequestInvoker(_RequestQmonData).BeginInvoke(Symbol, null, null);
        }
        public void UnsubscribeSymbol(string Symbol)
        {
            List<ItemInfo> items=  FindItemFromMap(new RFA_String(Symbol));
            foreach (ItemInfo itm in items)
            {
                ommConsumer.UnregisterClient(itm.ItemHandle);
                RemoveItemFromMap(itm.ItemHandle);
            }
        
        }
        public void UnsubscribeAll() { 
        
        }
        public void Shutdown() {

            if (ommConsumer != null) {
                ommConsumer.UnregisterClient(loginHandle);
                loginHandle = 0;
            }
            if (ommConnIntSpecHandle != 0) {
                ommConsumer.UnregisterClient(ommConnIntSpecHandle);
                ommConnIntSpecHandle = 0;
            }
            if (ommErrIntSpecHandle !=0){
                ommConsumer.UnregisterClient(ommErrIntSpecHandle);
                ommErrIntSpecHandle = 0;
            }
            ommConsumer.Destroy();
            ommConsumer = null;
            if (session != null) {
                session.Release();
                session = null;
            }
            if (cfgDataBase != null)
            {
                cfgDataBase.Release();
                cfgDataBase = null;
            }
            if (appCfgDataBase != null)
            {
                appCfgDataBase.Release();
                appCfgDataBase = null;
            }
            if (appStgCfgDataBase != null)
            {
                appStgCfgDataBase.Destroy();
                appStgCfgDataBase = null;
            }
            if ((!Context.Uninitialize()) && (Context.InitializedCount == 0))
            {
                // report problem if and ONLY if Context.Uninitialize() returned false
                // AND Context.InitializedCount returns zero;
                // if Context.InitializedCount returns more than zero,
                // RFA did not attempt to uninitialize its context
            }
          
        }
        public List<RFA_String> FormBatch(string Symbol) {
            List<RFA_String> BatchList = new List<RFA_String>();
            com.omexsystems.MSDsvc msdsv = new TRQuoteCore.com.omexsystems.MSDsvc();

            


            TRQuoteCore.Symbology.DetectedSymbology tempSymbol = Symbology.Symbology.DetectSymbology(Symbol);
            switch (tempSymbol) { 
                case TRQuoteCore.Symbology.DetectedSymbology.Index:
                    BatchList.Add(new RFA_String(Symbol.Replace("=","")));
                    break;

                case TRQuoteCore.Symbology.DetectedSymbology.Equity:
                        string primarySymbol = string.Empty;
                            object[] simpleobj = msdsv.getDiffInfo(Symbol, "msd_ticker","All","", "wbet_master", "monitor-khi", "wbetmasmk!03", "PROD");

                            com.omexsystems.MSD targetobj = null;


                            foreach (com.omexsystems.MSD obj in simpleobj)
                            {
                                if (obj.msd_ticker == Symbol)
                                {
                                    targetobj = obj;
                                }
                            }

                            primarySymbol = Symbol + Symbology.Symbology.FormatExchangeCode(targetobj.msd_eqy_prim_exch_shrt);
                            if (primarySymbol.EndsWith(".O"))
                            {
                                BatchList.Add(new RFA_String(primarySymbol));
                            }
                            else {
                                BatchList.Add(new RFA_String(Symbol));
                            
                            }
                       
                    //BatchList.Add(new RFA_String(Symbol + ".N"));
                    //BatchList.Add(new RFA_String(Symbol + ".A"));
                    //BatchList.Add(new RFA_String(Symbol + ".P"));
                    //BatchList.Add(new RFA_String(Symbol + ".OQ"));
                    //BatchList.Add(new RFA_String(Symbol + ".O"));
                    //BatchList.Add(new RFA_String(Symbol + ".C"));
                    //BatchList.Add(new RFA_String(Symbol + ".MW"));
                    //BatchList.Add(new RFA_String(Symbol + ".TH"));
                    //BatchList.Add(new RFA_String(Symbol + ".TH"));
                    //BatchList.Add(new RFA_String(Symbol + ".Y"));
                    //BatchList.Add(new RFA_String(Symbol + ".ZY"));
                    //BatchList.Add(new RFA_String(Symbol + ".Z"));
                    //BatchList.Add(new RFA_String(Symbol + ".DG"));
                    //BatchList.Add(new RFA_String(Symbol + ".B"));
                    //BatchList.Add(new RFA_String(Symbol + ".DF"));
                    //BatchList.Add(new RFA_String(Symbol + ".NB"));
                    //BatchList.Add(new RFA_String(Symbol + ".PH"));
                         
                    break;
                case TRQuoteCore.Symbology.DetectedSymbology.EquityOptions:
                    BatchList.Add(new RFA_String(TRQuoteCore.Symbology.Symbology.FormatOptionRIC(Symbol)));
                    break;
                case TRQuoteCore.Symbology.DetectedSymbology.PinkSheets:
                    Symbol = Symbol.Split('.')[0];
                    
                    BatchList.Add(new RFA_String(Symbol+".PQ"));
                    BatchList.Add(new RFA_String(Symbol +".PK"));
                   //BatchList.Add(new RFA_String("0#NSRGY.PK"));
                    //BatchList.Add(new RFA_String(Symbol + ".U"));
                    //BatchList.Add(new RFA_String(Symbol + ".S"));
                    
                    break;

            }
            


            return BatchList;
        }
        public List<RFA_String> CreateLevel2FieldList(bool reqImbalance) {
            List<RFA_String> FieldLevel2List = new List<RFA_String>();
            FieldLevel2List.Add(new RFA_String("3735"));
            FieldLevel2List.Add(new RFA_String("3879")); // Trade High
            FieldLevel2List.Add(new RFA_String("3878")); // Trade Low
            FieldLevel2List.Add(new RFA_String("3422")); // Symbol
            FieldLevel2List.Add(new RFA_String("4418")); //Accumulated Volume of pre-open market FID_PRE_POST_MARKET_CUMULATIVE_VOLUME) in activ.
            FieldLevel2List.Add(new RFA_String("19"));// Open Price open in activ
            FieldLevel2List.Add(new RFA_String("6"));//TRDPRC_1 trade in activ
            FieldLevel2List.Add(new RFA_String("18")); //Trade TIme
            FieldLevel2List.Add(new RFA_String("6638")); //Trade Exchange
            FieldLevel2List.Add(new RFA_String("55")); // Lot size equivalent to trade size in activ
            FieldLevel2List.Add(new RFA_String("32")); //Accumelated volume
            FieldLevel2List.Add(new RFA_String("383")); // previous day's accumulated volume. equivalent to FID_PREVIOUS_CUMULATIVE_VOLUME in activ
            FieldLevel2List.Add(new RFA_String("22"));//Bid
            FieldLevel2List.Add(new RFA_String("30"));// Bid Size
            FieldLevel2List.Add(new RFA_String ("266"));//Bid Time
            FieldLevel2List.Add(new RFA_String("6636"));// Bid exchange
            FieldLevel2List.Add(new RFA_String("25")); // Ask
            FieldLevel2List.Add(new RFA_String("31")); // Ask size
            FieldLevel2List.Add(new RFA_String("267")); // Ask time 
            FieldLevel2List.Add(new RFA_String("6637"));// Ask Exchange
            if (reqImbalance) {
                FieldLevel2List.Add(new RFA_String("4337")); //Total number of shares that are eligible to be matched at the current reference price equivalent to FID_PAIRED_SHARES of activ
                FieldLevel2List.Add(new RFA_String("4334")); // Equivalent to FID_FAR_PRICE of activ  Far Indicative Clearing Price - The far price level at which buy and sell orders may match during an auction period. Populated during order imbalances.
                FieldLevel2List.Add(new RFA_String("4335")); // Equivalent to FID_NEAR_PRICE of activ Near Indicative Clearing Price - the near crossing price at which orders in the opening/closing book and continuous book clear against each other. Populated during order imbalances.
                FieldLevel2List.Add(new RFA_String("4336")); // Equivalent to FID_CURRENT_REFERENCE_PRICE of activ Price for which the number of Paired Shares and number of Imbalance Shares are calculated.
                FieldLevel2List.Add(new RFA_String("4218")); // Equivalent to FID_IMBALANCE_BUY_VOLUME of activ, Total trade volume on the buy side
                FieldLevel2List.Add(new RFA_String("4224")); //Equivalent to FID_IMBALANCE_SELL_VOLUME of activ, Total trade volume on the sell side
                //Fields couldn't be found FID_IMBALANCE_VOLUME_TIME,FID_CROSS_TYPE, FID_CLEARING_PRICE, FID_CLOSING_ONLY_CLEARING_PRICE
            }
            
            return FieldLevel2List;


        }
        public List<RFA_String> CreateFieldList() {
            List<RFA_String> FieldList = new List<RFA_String>();
            FieldList.Add(new RFA_String("3600"));
            FieldList.Add(new RFA_String("2"));
            FieldList.Add(new RFA_String("3"));
            FieldList.Add(new RFA_String("3655"));
            FieldList.Add(new RFA_String("1709"));
            FieldList.Add(new RFA_String("19"));
            FieldList.Add(new RFA_String("655"));
            FieldList.Add(new RFA_String("15"));
            FieldList.Add(new RFA_String("2178"));
            FieldList.Add(new RFA_String("4470"));
            FieldList.Add(new RFA_String("6636"));
            FieldList.Add(new RFA_String("6637"));
            FieldList.Add(new RFA_String("6638"));
            FieldList.Add(new RFA_String("25"));
            FieldList.Add(new RFA_String("22"));
            FieldList.Add(new RFA_String("30"));
            FieldList.Add(new RFA_String("31"));
            FieldList.Add(new RFA_String("11"));
            FieldList.Add(new RFA_String("12"));
            FieldList.Add(new RFA_String("13"));
            FieldList.Add(new RFA_String("3265"));
            FieldList.Add(new RFA_String("3266"));
            FieldList.Add(new RFA_String("6"));
            FieldList.Add(new RFA_String("18"));
            FieldList.Add(new RFA_String("266"));
            FieldList.Add(new RFA_String("267"));
            FieldList.Add(new RFA_String("3404"));
            FieldList.Add(new RFA_String("71"));
            FieldList.Add(new RFA_String("18"));
            FieldList.Add(new RFA_String("56"));
            FieldList.Add(new RFA_String("3824"));
            FieldList.Add(new RFA_String("32"));
            FieldList.Add(new RFA_String("3404"));
            FieldList.Add(new RFA_String("3677"));
            FieldList.Add(new RFA_String("50"));
            FieldList.Add(new RFA_String("21"));
            FieldList.Add(new RFA_String("44"));
            FieldList.Add(new RFA_String("379"));
            FieldList.Add(new RFA_String("5"));
            FieldList.Add(new RFA_String("18"));
            FieldList.Add(new RFA_String("16"));
            FieldList.Add(new RFA_String("17"));
            FieldList.Add(new RFA_String("607"));
            FieldList.Add(new RFA_String("608"));
            FieldList.Add(new RFA_String("1067"));
            FieldList.Add(new RFA_String("16"));
            FieldList.Add(new RFA_String("3804"));
            FieldList.Add(new RFA_String("3854"));
            FieldList.Add(new RFA_String("4150"));
            FieldList.Add(new RFA_String("4147"));
            FieldList.Add(new RFA_String("1025"));
            FieldList.Add(new RFA_String("1026"));
            FieldList.Add(new RFA_String("4200"));
            FieldList.Add(new RFA_String("66"));
            FieldList.Add(new RFA_String("67"));
            FieldList.Add(new RFA_String("55"));
            FieldList.Add(new RFA_String("3803"));
            FieldList.Add(new RFA_String("56"));
            FieldList.Add(new RFA_String("6638"));
            FieldList.Add(new RFA_String("8981"));
            FieldList.Add(new RFA_String("90"));
            FieldList.Add(new RFA_String("91"));
            FieldList.Add(new RFA_String("1075"));
            FieldList.Add(new RFA_String("1076"));
            FieldList.Add(new RFA_String("3254"));
            FieldList.Add(new RFA_String("7582"));
            FieldList.Add(new RFA_String("2744"));
            FieldList.Add(new RFA_String("5350"));
            FieldList.Add(new RFA_String("3648"));
            FieldList.Add(new RFA_String("2142"));
            FieldList.Add(new RFA_String("7623")); // Volatility 
            FieldList.Add(new RFA_String("7679"));
            FieldList.Add(new RFA_String("6241"));
            FieldList.Add(new RFA_String("106"));
            FieldList.Add(new RFA_String("2150"));
            
            return FieldList;
        }
       
        public void _RequestQmonData(string Symbol)
        {
//            quoteList.Clear();
            long batchReqHandle = 0;

            List<RFA_String> strlist = FormBatch(Symbol);

            ReqMsg reqMsg = new ReqMsg();
            reqMsg.MsgModelType = RDM.MESSAGE_MODEL_TYPES.MMT_MARKET_PRICE;
            reqMsg.InteractionType = ReqMsg.InteractionTypeFlag.InitialImage | ReqMsg.InteractionTypeFlag.InterestAfterRefresh;

            AttribInfo attribInfo = new AttribInfo();
            attribInfo.ServiceName = confgVariables.ServiceName;

            ElementList attribElementList = new ElementList();
            if (confgVariables.SendAttribInfo)
            {
                //Encode the List name into the AttribInfo
                ElementEntry element1 = new ElementEntry();
                DataBuffer elementData1 = new DataBuffer();
                ElementListWriteIterator elwiter1 = new ElementListWriteIterator();
                elwiter1.Start(attribElementList);

                RFA_String listName = new RFA_String();
                listName.Set("Request List");
                element1.Name = listName;
                RFA_String stringListValue = new RFA_String(Symbol);
                elementData1.SetFromString(stringListValue, DataBuffer.DataBufferEnum.StringAscii);
                element1.Data = elementData1;
                elwiter1.Bind(element1);
                elwiter1.Complete();
                attribInfo.Attrib = attribElementList;
            }
            reqMsg.AttribInfo = attribInfo;

            //Set Batch Request Flag
            reqMsg.IndicationMask = ReqMsg.IndicationMaskFlag.Batch;

            //Set itemList in payload.
            ElementList elementList = new ElementList();
            ElementEntry element = new ElementEntry();
            ElementListWriteIterator elwiter = new ElementListWriteIterator();

            elwiter.Start(elementList);

            ArrayWriteIterator arrWIt = new ArrayWriteIterator();
            ThomsonReuters.RFA.Data.Array elementData = new ThomsonReuters.RFA.Data.Array();
            //Encode a ItemList for batch
            arrWIt.Start(elementData);

            DataBuffer dataBuffer = new DataBuffer();
            ArrayEntry arrayEntry = new ArrayEntry();
            for (int i = 0; i < strlist.Count(); i++)
            {
                dataBuffer.Clear();
                arrayEntry.Clear();
                dataBuffer.SetFromString(strlist[i], DataBuffer.DataBufferEnum.StringAscii);
                arrayEntry.Data = dataBuffer;
                arrWIt.Bind(arrayEntry);
            }
            arrWIt.Complete();

            element.Name = RDM.REQUEST_MSG_PAYLOAD_ELEMENT_NAME.ENAME_BATCH_ITEM_LIST;
            element.Data = elementData;
            elwiter.Bind(element);
            int viewFieldCount = 0;
            List<RFA_String> viewFieldIdList = CreateFieldList();
            viewFieldCount = viewFieldIdList.Count();
            if (viewFieldCount > 0)
            {
                //Set view Request Flag
                reqMsg.IndicationMask = (byte)(reqMsg.IndicationMask | ReqMsg.IndicationMaskFlag.View);
                element.Clear();
                element.Name = RDM.REQUEST_MSG_PAYLOAD_ELEMENT_NAME.ENAME_VIEW_TYPE;
                dataBuffer.Clear();
                dataBuffer.SetUInt(RDM.VIEW_TYPES.VT_FIELD_ID_LIST, DataBuffer.DataBufferEnum.UInt);
                element.Data = dataBuffer;
                elwiter.Bind(element);

                //Encode the ViewData
                element.Clear();
                elementData.Clear();
                arrWIt.Start(elementData);

                for (int i = 0; i < viewFieldCount; i++)
                {
                    dataBuffer.Clear();
                    arrayEntry.Clear();
                    dataBuffer.SetFromString(viewFieldIdList[i], DataBuffer.DataBufferEnum.Int);
                    arrayEntry.Data = dataBuffer;
                    arrWIt.Bind(arrayEntry);
                }
                arrWIt.Complete();

                element.Name = RDM.REQUEST_MSG_PAYLOAD_ELEMENT_NAME.ENAME_VIEW_DATA;
                element.Data = elementData;
                elwiter.Bind(element);
            }

            elwiter.Complete();
            reqMsg.Payload = elementList;
            OMMItemIntSpec ommItemIntSpec = new OMMItemIntSpec();
            ommItemIntSpec.Msg = reqMsg;
            //To add batch closure, replace NULL in this RegisterClient call similar to the below example:
            //batchReqHandle = ommConsumer.RegisterClient(eventQueue, ommItemIntSpec, this, batchClosure );
            batchReqHandle = ommConsumer.RegisterClient(null, ommItemIntSpec, this, null);

            //Add the handle for the batch to the itemMap
            if (Symbol.StartsWith(".")) {
                ItemInfo optionItem = new ItemInfo();
                optionItem.ItemHandle = 0;
                optionItem.ItemName = new RFA_String(strlist[0].ToString());
                optionItem.OriginalSymbol = Symbol;
                optionItem.Closed = false;
                optionItem.isoption = true;
                if (!itemList.ContainsKey(strlist[0].ToString()))
                {
                    itemList.Add(strlist[0].ToString(), optionItem);
                }
                return;
    
            }
            ItemInfo anItem = new ItemInfo();
            anItem.ItemHandle = 0;
            anItem.ItemName = new RFA_String(strlist[0].ToString());
            anItem.OriginalSymbol = Symbol;   
            anItem.Closed = false;
            if (!itemList.ContainsKey(strlist[0].ToString()))
            {
                itemList.Add(strlist[0].ToString(), anItem);
                
            }
                
            
            //Add each of the items in that batch to the itemMap
            for (int i = 1; i < strlist.Count(); i++)
            {
                ItemInfo eachItem = new ItemInfo();
                eachItem.ItemHandle = 0;
                eachItem.ItemName = new RFA_String(strlist[i]);
                eachItem.OriginalSymbol = Symbol;
                anItem.Closed = false;
                if (!itemList.ContainsKey(strlist[i].ToString()))
                {
                    itemList.Add(strlist[i].ToString(), eachItem);

                }
            
            }
            
        }
        public void ProcessEvent(Event evnt)
        {
            switch (evnt.Type)
            {
                case SessionLayerEventTypeEnum.ConnectionEvent:
                    ProcessConnectionEvent(evnt as OMMConnectionEvent);
                    break;
                case SessionLayerEventTypeEnum.OMMItemEvent:
                    ProcessOMMItemEvent(evnt as OMMItemEvent);
                    break;
                case LoggerEventTypeEnum.LoggerNotifyEvent:
                    ProcessLoggerNotifyEvent(evnt as LoggerNotifyEvent);
                    break;
                default:
                    break;
            }
            if (evnt.IsEventStreamClosed)
            {
              //  RFA_String text = OMMStrings.EventTypeToString(evnt.Type);
            //    DisableItemListEntry(evnt.Handle, true);
//            
            }

        }
        private void ProcessOMMItemEvent(OMMItemEvent evnt)
        {
            Msg msg = evnt.Msg;
            switch (msg.MsgType)
            {
                case MsgTypeEnum.RespMsg:
                    ProcessRespMsg(evnt, msg as RespMsg);
                    break;
                case MsgTypeEnum.GenericMsg:
                   // ProcessGenericMsg(evnt, msg as GenericMsg);
                    break;
                case MsgTypeEnum.AckMsg:
                    //ProcessAckMsg(evnt, msg as AckMsg);
                    break;
                default:
                    //AppUtil.Log(AppUtil.LEVEL.WARN, string.Format("<- Received event with unknown message type: {0}", msg.MsgType));
                    break;
            }
        }
        private void ProcessRespMsg(OMMItemEvent evnt, RespMsg respMsg)
        {
            switch (respMsg.MsgModelType)
            {
                case RDM.MESSAGE_MODEL_TYPES.MMT_LOGIN:
                    ProcessLoginResponse(evnt, respMsg);
                    break;
                case RDM.MESSAGE_MODEL_TYPES.MMT_MARKET_PRICE:
                    ProcessMarketPriceResponse(evnt, respMsg);
                    break;
                default:
         //           AppUtil.Log(AppUtil.LEVEL.WARN, string.Format("<- Received unhandled OMMItemEvent msgModelType: {0}", respMsg.MsgModelType));
                    break;
            }
        }
        private List<RFA_String> createMarketExchanges(string Symbol) {
            List<RFA_String> listExchanges = new List<RFA_String>();
            listExchanges.Add(new RFA_String(Symbol + ".N"));
            listExchanges.Add(new RFA_String(Symbol + ".B"));
            listExchanges.Add(new RFA_String(Symbol + ".C"));
            listExchanges.Add(new RFA_String(Symbol + ".MW"));
            listExchanges.Add(new RFA_String(Symbol + ".P"));
            listExchanges.Add(new RFA_String(Symbol + ".TH"));
            listExchanges.Add(new RFA_String(Symbol + ".DF"));
            listExchanges.Add(new RFA_String(Symbol + ".II"));
            listExchanges.Add(new RFA_String(Symbol + ".W"));
            listExchanges.Add(new RFA_String(Symbol + ".Z"));
            listExchanges.Add(new RFA_String(Symbol + ".O"));
            listExchanges.Add(new RFA_String(Symbol + ".OQ"));



            return listExchanges;
        
        }
        public void RequestLevel2Data(string Symbol) {
           //if (RegisterLevel2(Symbol) > 0) {
                RequestMarketMakers("0#"+Symbol+".O");

            //}
        
        }
        private long  RegisterLevel2(string Symbol) {
            isLevel2 = true;
            long ReqHandle = 0;

            List<RFA_String> strlist = createMarketExchanges(Symbol);

            ReqMsg reqMsg = new ReqMsg();
            reqMsg.MsgModelType = RDM.MESSAGE_MODEL_TYPES.MMT_MARKET_PRICE;
            reqMsg.InteractionType = ReqMsg.InteractionTypeFlag.InitialImage | ReqMsg.InteractionTypeFlag.InterestAfterRefresh;
            
            AttribInfo attribInfo = new AttribInfo();
            attribInfo.ServiceName = confgVariables.ServiceName;

            ElementList attribElementList = new ElementList();
            if (confgVariables.SendAttribInfo)
            {
                //Encode the List name into the AttribInfo
                ElementEntry element1 = new ElementEntry();
                DataBuffer elementData1 = new DataBuffer();
                ElementListWriteIterator elwiter1 = new ElementListWriteIterator();
                elwiter1.Start(attribElementList);

                RFA_String listName = new RFA_String();
                listName.Set("Request List");
                element1.Name = listName;
                RFA_String stringListValue = new RFA_String(Symbol);
                elementData1.SetFromString(stringListValue, DataBuffer.DataBufferEnum.StringAscii);
                element1.Data = elementData1;
                elwiter1.Bind(element1);
                elwiter1.Complete();
                attribInfo.Attrib = attribElementList;
            }
           
            reqMsg.AttribInfo = attribInfo;
            reqMsg.IndicationMask = ReqMsg.IndicationMaskFlag.Batch;



            ElementList elementList = new ElementList();
            ElementEntry element = new ElementEntry();
            ElementListWriteIterator elwiter = new ElementListWriteIterator();
            elwiter.Start(elementList);

           
            ArrayWriteIterator arrWIt = new ArrayWriteIterator();
            ThomsonReuters.RFA.Data.Array elementData = new ThomsonReuters.RFA.Data.Array();
            
            
            arrWIt.Start(elementData);

            DataBuffer dataBuffer = new DataBuffer();
            ArrayEntry arrayEntry = new ArrayEntry();
            
            List<RFA_String> fieldlist = CreateFieldList();


            for (int i = 0; i < strlist.Count(); i++)
            {
                dataBuffer.Clear();
                arrayEntry.Clear();
                dataBuffer.SetFromString(strlist[i], DataBuffer.DataBufferEnum.StringAscii);
                arrayEntry.Data = dataBuffer;
                arrWIt.Bind(arrayEntry);
            }
            arrWIt.Complete();
            element.Name = RDM.REQUEST_MSG_PAYLOAD_ELEMENT_NAME.ENAME_BATCH_ITEM_LIST;
            element.Data = elementData;
            elwiter.Bind(element);

            reqMsg.IndicationMask = (byte)(reqMsg.IndicationMask | ReqMsg.IndicationMaskFlag.View);
            element.Clear();
            element.Name = RDM.REQUEST_MSG_PAYLOAD_ELEMENT_NAME.ENAME_VIEW_TYPE;
            dataBuffer.Clear();
            dataBuffer.SetUInt(RDM.VIEW_TYPES.VT_FIELD_ID_LIST, DataBuffer.DataBufferEnum.UInt);
            element.Data = dataBuffer;
            elwiter.Bind(element);

            element.Clear();
            elementData.Clear();
            arrWIt.Start(elementData);
            for (int i = 0; i < fieldlist.Count(); i++) {
                dataBuffer.Clear();
                arrayEntry.Clear();
                dataBuffer.SetFromString(fieldlist[i], DataBuffer.DataBufferEnum.Int);
                arrayEntry.Data = dataBuffer;
                arrWIt.Bind(arrayEntry);
                
            }
            arrWIt.Complete();
            element.Name = RDM.REQUEST_MSG_PAYLOAD_ELEMENT_NAME.ENAME_VIEW_DATA;
            element.Data = elementData;
            elwiter.Bind(element);

            elwiter.Complete();

            reqMsg.Payload = elementList;

            OMMItemIntSpec ommItemIntSpec = new OMMItemIntSpec();
            ommItemIntSpec.Msg = reqMsg; 
            ReqHandle= ommConsumer.RegisterClient(null,ommItemIntSpec,this );

            ItemInfo anItem = new ItemInfo();
            anItem.ItemHandle = 0;
            anItem.ItemName = new RFA_String(strlist[0].ToString());
            anItem.OriginalSymbol = Symbol;
            anItem.Closed = false;
            if (!itemList.ContainsKey(strlist[0].ToString()))
            {
                itemList.Add(strlist[0].ToString(), anItem);

            }


            //Add each of the items in that batch to the itemMap
            for (int i = 1; i < strlist.Count(); i++)
            {
                ItemInfo eachItem = new ItemInfo();
                eachItem.ItemHandle = 0;
                eachItem.ItemName = new RFA_String(strlist[i]);
                eachItem.OriginalSymbol = Symbol;
                anItem.Closed = false;
                if (!itemList.ContainsKey(strlist[i].ToString()))
                {
                    itemList.Add(strlist[i].ToString(), eachItem);

                }

            }
            

            return ReqHandle;

            
        }
        private void RequestMarketMakers(string Symbol) {
            string temp = string.Empty;
            switch (Symbology.Symbology.DetectSymbology(Symbol))
            {
                case TRQuoteCore.Symbology.DetectedSymbology.Equity:

                    temp = Symbol ;
                    break;
            }

            long mmhandle = 0;
            ReqMsg reqMsg = new ReqMsg();
            AttribInfo attribInfo = new AttribInfo();
            reqMsg.MsgModelType = RDM.MESSAGE_MODEL_TYPES.MMT_MARKET_PRICE;
            reqMsg.InteractionType = ReqMsg.InteractionTypeFlag.InitialImage;
            attribInfo.NameType = RDM.INSTRUMENT_NAME_TYPES.INSTRUMENT_NAME_RIC;
            attribInfo.Name = new RFA_String(Symbol);
            attribInfo.ServiceName = confgVariables.ServiceName;
            reqMsg.AttribInfo = attribInfo;
            OMMItemIntSpec ommItemIntSpec = new OMMItemIntSpec();
            ommItemIntSpec.Msg = reqMsg;

            mmhandle = ommConsumer.RegisterClient(null, ommItemIntSpec, this, new RFA_String(temp));

            reqMsg.AttribInfo = attribInfo;
            ItemInfo anItem = new ItemInfo();
            anItem.ItemHandle = mmhandle;
            anItem.ItemName = new RFA_String(temp);
            anItem.OriginalSymbol = Symbol;
            anItem.Closed = false;
            if (!itemList.ContainsKey(temp))
            {
                itemList.Add(temp, anItem);

            }

            
        }
        private void RegisterMarketMaker(List<RFA_String> MMIDs) {
            string temp = string.Empty;
         
            long mmhandle = 0;
            ReqMsg reqMsg = new ReqMsg();
            AttribInfo attribInfo = new AttribInfo();
            List<RFA_String> marketFields = CreateLevel2FieldList(false);
            reqMsg.MsgModelType = RDM.MESSAGE_MODEL_TYPES.MMT_MARKET_PRICE;
            reqMsg.InteractionType = ReqMsg.InteractionTypeFlag.InitialImage | ReqMsg.InteractionTypeFlag.InterestAfterRefresh;
            

            attribInfo.NameType = RDM.INSTRUMENT_NAME_TYPES.INSTRUMENT_NAME_RIC;
            attribInfo.Name = new RFA_String(temp);
            attribInfo.ServiceName = confgVariables.ServiceName;

            ElementList attribElementList = new ElementList();
            //if (true)
            //{
            //    //Encode the List name into the AttribInfo
            //    ElementEntry attribElement = new ElementEntry();
            //    DataBuffer attribElementData = new DataBuffer();
            //    ElementListWriteIterator attribelwiter = new ElementListWriteIterator();
            //    RFA_String listName = new RFA_String();

            //    attribelwiter.Start(attribElementList);
            //    listName.Set("Request List");
            //    attribElement.Name = listName;
            //    RFA_String Name = new RFA_String("Item Name");
            //    attribElementData.SetFromString(new RFA_String(temp), DataBuffer.DataBufferEnum.StringAscii);
            //    attribElement.Data = attribElementData;
            //    attribelwiter.Bind(attribElement);
            //    attribelwiter.Complete();
            //    attribInfo.Attrib = attribElementList;
            //}
            reqMsg.AttribInfo = attribInfo;
            reqMsg.IndicationMask = ReqMsg.IndicationMaskFlag.Batch;

            ElementList elementList = new ElementList();
          
            ElementEntry element = new ElementEntry();
            ElementListWriteIterator elwiter = new ElementListWriteIterator();
            DataBuffer dataBuffer = new DataBuffer();
            elwiter.Start(elementList);

            //Encode a ViewType
          
            ArrayWriteIterator arrWIt = new ArrayWriteIterator();
            ThomsonReuters.RFA.Data.Array elementData = new ThomsonReuters.RFA.Data.Array();
            ArrayEntry arrayEntry = new ArrayEntry();
            arrWIt.Start(elementData);

            for (int i = 0; i < MMIDs.Count(); i++) {
                dataBuffer.Clear();
                arrayEntry.Clear();
                dataBuffer.SetFromString(MMIDs[i], DataBuffer.DataBufferEnum.StringAscii);
                arrayEntry.Data = dataBuffer;
                arrWIt.Bind(arrayEntry);
            }
            arrWIt.Complete();
            element.Name = RDM.REQUEST_MSG_PAYLOAD_ELEMENT_NAME.ENAME_BATCH_ITEM_LIST;
            element.Data = elementData;
            elwiter.Bind(element);
           
            //Encode the ViewData
            reqMsg.IndicationMask = (byte)(reqMsg.IndicationMask | ReqMsg.IndicationMaskFlag.View);
               
            element.Clear();
            element.Name = RDM.REQUEST_MSG_PAYLOAD_ELEMENT_NAME.ENAME_VIEW_TYPE;
            dataBuffer.Clear();
            dataBuffer.SetUInt(RDM.VIEW_TYPES.VT_FIELD_ID_LIST, DataBuffer.DataBufferEnum.UInt);
            element.Data = dataBuffer;
            elwiter.Bind(element);

            element.Clear();
            elementData.Clear();
            arrWIt.Start(elementData);


            for (int i = 0; i < marketFields.Count(); i++) {
                dataBuffer.Clear();
                arrayEntry.Clear();
                dataBuffer.SetFromString(marketFields[i], DataBuffer.DataBufferEnum.Int);
                arrayEntry.Data = dataBuffer;
                arrWIt.Bind(arrayEntry);
            }
            arrWIt.Complete();
            element.Name = RDM.REQUEST_MSG_PAYLOAD_ELEMENT_NAME.ENAME_VIEW_DATA;
            element.Data = elementData;
            elwiter.Bind(element);
            elwiter.Complete();
            reqMsg.Payload = elementList;
          
            OMMItemIntSpec ommItemIntSpec = new OMMItemIntSpec();
            ommItemIntSpec.Msg = reqMsg;

            mmhandle=  ommConsumer.RegisterClient(null, ommItemIntSpec, this,null);


            

           
        }
        private void ProcessLoginResponse(OMMItemEvent evnt, RespMsg respMsg)
        {
            RespStatus status = respMsg.RespStatus;
            RFA_String text = new RFA_String(); // OMMStrings.RespStatusToString(status);

            //For a Login Response examine the stream state and data state:
            //If stream state is open and data state is OK then we have a successful Login
            //If stream state is open and data state is NOT OK then we have a pending Login
            switch (respMsg.RespType)
            {
                case RespMsg.RespTypeEnum.Refresh:
                    if ((respMsg.HintMask & RespMsg.HintMaskFlag.RespStatus) != 0)
                    {
                        if ((status.StreamState == RespStatus.StreamStateEnum.Open) &&
                            (status.DataState == RespStatus.DataStateEnum.Ok))
                        {
                            callback.OnConnectionStatus(ConnectionStatus.Connected);
                     
                         //       AppUtil.Log(AppUtil.LEVEL.TRACE, string.Format("<- Received MMT_LOGIN Refresh - Login Accepted{0}", text.ToString()));
                         //     ProcessLoginSuccess();
                        }
                        else if (status.StreamState == RespStatus.StreamStateEnum.Open)
                        {
                         //   AppUtil.Log(AppUtil.LEVEL.WARN, string.Format("<- Received MMT_LOGIN Refresh - Login Pending{0}", text.ToString()));
                        }
                        else
                        {
                            callback.OnConnectionStatus(ConnectionStatus.Bad_User);
                        }
                    }
                    break;
                case RespMsg.RespTypeEnum.Status:
                    if ((respMsg.HintMask & RespMsg.HintMaskFlag.RespStatus) != 0)
                    {
                        if ((status.StreamState == RespStatus.StreamStateEnum.Open) &&
                            (status.DataState == RespStatus.DataStateEnum.Ok))
                        {
                            callback.OnConnectionStatus(ConnectionStatus.Ready);
                           // AppUtil.Log(AppUtil.LEVEL.TRACE, string.Format("<- Received MMT_LOGIN Status - Login Accepted{0}", text.ToString()));
                           // ProcessLoginSuccess();
                        }
                        else if (status.StreamState == RespStatus.StreamStateEnum.Open)
                        {
                            callback.OnConnectionStatus(ConnectionStatus.Connected);
                           // AppUtil.Log(AppUtil.LEVEL.WARN, string.Format("<- Received MMT_LOGIN Status - Login Pending{0}", text.ToString()));
                        }
                        else
                        {
                           // AppUtil.Log(AppUtil.LEVEL.ERR, string.Format("<- Received MMT_LOGIN Status - Login Denied{0}", text.ToString()));
                            callback.OnConnectionStatus(ConnectionStatus.Bad_User);
                        }
                    }
                    else
                    {
                        //AppUtil.Log(AppUtil.LEVEL.ERR, "<- Received MMT_LOGIN Status - No RespStatus");
                    }
                    break;
                case RespMsg.RespTypeEnum.Update:
                    // in the future we could receive an update with a new permission profile
                    ///AppUtil.Log(AppUtil.LEVEL.ERR, "<- Received MMT_LOGIN Update");
                    break;
                default:
                    //AppUtil.Log(AppUtil.LEVEL.ERR, string.Format("<- Received a non-supported MMT_LOGIN RespMsg type: {0}", respMsg.RespType));
                    break;
            }
        }

        private void ProcessLoggerNotifyEvent(LoggerNotifyEvent loggerEvent)
        {
            RFA_String text = new RFA_String(loggerEvent.MessageText);
            text.TrimWhitespace();
            //AppUtil.LEVEL level = AppUtil.LEVEL.TRACE;
            switch (loggerEvent.Severity)
            {
                case CommonErrorSeverityTypeEnum.Success:
                    //level = AppUtil.LEVEL.TRACE;
                    break;
                case CommonErrorSeverityTypeEnum.Information:
                  //  level = AppUtil.LEVEL.TRACE;
                    break;
                case CommonErrorSeverityTypeEnum.Warning:
                //    level = AppUtil.LEVEL.WARN;
                    break;
                case CommonErrorSeverityTypeEnum.Error:
              //      level = AppUtil.LEVEL.ERR;
                    break;
            }
            //AppUtil.Log(level, string.Format("<- Received LoggerNotifyEvent:\r\n    {0}\r\n", text.ToString()));
        }

        private void ProcessConnectionEvent(OMMConnectionEvent ommConnectionEvent)
        {
            ThomsonReuters.RFA.SessionLayer.ConnectionStatus connectionStatus = ommConnectionEvent.Status;
            RFA_String text = new RFA_String();
            text.Append("<- Received ConnectionEvent: Connection\r\n    ");
            text.Append(ommConnectionEvent.ConnectionName);
            if (connectionStatus.State == ThomsonReuters.RFA.SessionLayer.ConnectionStatus.StateEnum.Up)
            {
                callback.OnConnectionStatus(ConnectionStatus.Ready);
       
                text.Append(" is UP!\r\n");
            }
            else
            {
                text.Append(" is DOWN!\r\n");
                callback.OnConnectionStatus(ConnectionStatus.Connection_Error);
       
            }
           // AppUtil.Log(AppUtil.LEVEL.INFO, text.ToString());
        }
        private void ProcessMarketPriceResponse(OMMItemEvent evnt, RespMsg respMsg)
        {
            RFA_String text = new RFA_String("<- Received MMT_MARKET_PRICE ");
            if (evnt.Closure != null)
            {
                text.Append(" ");
                text.Append(evnt.Closure as RFA_String);
            }
            AttribInfo attribInfo = respMsg.AttribInfo;
            long itemHandle = evnt.Handle;
            ItemInfo theItem = null;

            if ((respMsg.RespType == RespMsg.RespTypeEnum.Refresh || respMsg.RespType == RespMsg.RespTypeEnum.Status) && ((respMsg.HintMask & RespMsg.HintMaskFlag.AttribInfo) != 0))
            {
                theItem = FindItemFromMap(respMsg.AttribInfo.Name, itemHandle);
                attribInfo = respMsg.AttribInfo;
                byte hint = attribInfo.HintMask;
                if ((hint & AttribInfo.HintMaskFlag.ServiceName) != 0)
                {
                    text.Append("\r\n    serviceName : ");
                    text.Append(attribInfo.ServiceName);
                }
                if ((hint & AttribInfo.HintMaskFlag.Name) != 0)
                {
                    text.Append("\r\n    symbolName  : ");
                    text.Append(attribInfo.Name);
                }
            }
            else
            {
                theItem = FindItemFromMap(null, itemHandle);
            }

            Console.Write(respMsg.RespTypeNum);
            string temp = "";
            if (respMsg.RespType == RespMsg.RespTypeEnum.Update)
            {
                switch (respMsg.RespTypeNum)
                {
                    case RDM.INSTRUMENT_UPDATE_RESPONSE_TYPES.INSTRUMENT_UPDATE_QUOTE:
                        if ((respMsg.HintMask & RespMsg.HintMaskFlag.Payload) != 0)
                        {
                            theItem = FindItemFromMap(respMsg.AttribInfo.Name, itemHandle);
                                
                            if (theItem != null)
                            {
                                BBOQuoteData bbquoteData = (BBOQuoteData)decoder.DecodeData(respMsg.Payload, theItem.OriginalSymbol, new BBOQuoteDecoder());
                                callback.OnBBOQuote(bbquoteData);
                            }
                        }
                        break;
                    case RDM.INSTRUMENT_UPDATE_RESPONSE_TYPES.INSTRUMENT_UPDATE_TRADE:
                        if ((respMsg.HintMask & RespMsg.HintMaskFlag.Payload) != 0)
                        {
                            theItem = FindItemFromMap(respMsg.AttribInfo.Name, itemHandle);
                            if (theItem != null)
                            {
                                TradeData tradedata = (TradeData)decoder.DecodeData(respMsg.Payload, theItem.OriginalSymbol, new TradeDecoder());
                                callback.OnTrade(tradedata);
                            }

                        }
                        break;
                    case RDM.INSTRUMENT_UPDATE_RESPONSE_TYPES.INSTRUMENT_UPDATE_QUOTES_TRADE:
                        theItem = FindItemFromMap(null, evnt.Handle);
                            
                        if (theItem != null)
                        {
                           // TradeData tradedata = (TradeData)decoder.DecodeData(respMsg.Payload, theItem.OriginalSymbol, new TradeDecoder());
                           // callback.OnTrade(tradedata);
                        }
                        return;
                        break;
                    case RDM.INSTRUMENT_UPDATE_RESPONSE_TYPES.INSTRUMENT_UPDATE_UNSPECIFIED:
                        theItem = FindItemFromMap(respMsg.AttribInfo.Name, itemHandle);
                        if (theItem != null)
                        {
                            TradeData tradedata = (TradeData)decoder.DecodeData(respMsg.Payload, theItem.OriginalSymbol, new TradeDecoder());
                            callback.OnTrade(tradedata);
                        }
                        break;
                }
            }


            if ((respMsg.RespType == RespMsg.RespTypeEnum.Refresh || respMsg.RespType == RespMsg.RespTypeEnum.Status) && ((respMsg.HintMask & RespMsg.HintMaskFlag.AttribInfo) != 0))
            {
                byte hint = attribInfo.HintMask;
                if ((hint & AttribInfo.HintMaskFlag.ServiceName) != 0)
                {
                    text.Append("\r\n    serviceName : ");
                    text.Append(attribInfo.ServiceName);
                }
                if ((hint & AttribInfo.HintMaskFlag.Name) != 0)
                {
                    text.Append("\r\n    symbolName  : ");
                    text.Append(attribInfo.Name);
                }
                if ((respMsg.HintMask & RespMsg.HintMaskFlag.Payload) != 0)
                {

                    theItem = FindItemFromMap(respMsg.AttribInfo.Name, itemHandle);
                    if (respMsg.AttribInfo.Name.ToString().IndexOf("#") == 1) {
                        MarketMakerData objData = (MarketMakerData)decoder.DecodeData(respMsg.Payload, theItem.OriginalSymbol, new MMDecoder());
                        objMMData.Add(objData);
                        if (!String.IsNullOrEmpty(objData.NextLink)) {
                            RequestMarketMakers(objData.NextLink);
                        }
                        else if (String.IsNullOrEmpty(objData.NextLink) && objMMData.Count() >0){
                            List<RFA_String> MMIDs = new List<RFA_String>();
                            foreach (MarketMakerData objdata in objMMData) {
                                if (!String.IsNullOrEmpty(objData.MarketMaker1))
                                {
                                    MMIDs.Add(new RFA_String(objData.MarketMaker1));
                                }
                                if (!String.IsNullOrEmpty(objData.MarketMaker2))
                                {
                                    MMIDs.Add(new RFA_String(objData.MarketMaker2));
                                }
                                if (!String.IsNullOrEmpty(objData.MarketMaker3))
                                {
                                    MMIDs.Add(new RFA_String(objData.MarketMaker3));
                                }
                                if (!String.IsNullOrEmpty(objData.MarketMaker4))
                                {
                                    MMIDs.Add(new RFA_String(objData.MarketMaker4));
                                }
                                if (!String.IsNullOrEmpty(objData.MarketMaker5))
                                {
                                    MMIDs.Add(new RFA_String(objData.MarketMaker5));
                                }
                                if (!String.IsNullOrEmpty(objData.MarketMaker6))
                                {
                                    MMIDs.Add(new RFA_String(objData.MarketMaker6));
                                }
                                if (!String.IsNullOrEmpty(objData.MarketMaker7))
                                {
                                    MMIDs.Add(new RFA_String(objData.MarketMaker7));
                                }
                                if (!String.IsNullOrEmpty(objData.MarketMaker8))
                                {
                                    MMIDs.Add(new RFA_String(objData.MarketMaker8));
                                }
                                if (!String.IsNullOrEmpty(objData.MarketMaker9))
                                {
                                    MMIDs.Add(new RFA_String(objData.MarketMaker9));
                                }
                                if (!String.IsNullOrEmpty(objData.MarketMaker10))
                                {
                                    MMIDs.Add(new RFA_String(objData.MarketMaker10));
                                }
                                if (!String.IsNullOrEmpty(objData.MarketMaker11))
                                {
                                    MMIDs.Add(new RFA_String(objData.MarketMaker11));
                                
                                }
                                if (!String.IsNullOrEmpty(objData.MarketMaker12))
                                {
                                    MMIDs.Add(new RFA_String(objData.MarketMaker12));
                                }
                                if (!String.IsNullOrEmpty(objData.MarketMaker13))
                                {
                                    MMIDs.Add(new RFA_String(objData.MarketMaker13));
                                }
                                if (!String.IsNullOrEmpty(objData.MarketMaker14))
                                {
                                    MMIDs.Add(new RFA_String(objData.MarketMaker14));
                                }

                            }
                            RegisterMarketMaker(MMIDs);
                          }
                        
                        }

                    
                    else if (!Snapshotlist.ContainsKey(theItem.OriginalSymbol))
                    {
                        CompositeData objData = (CompositeData)decoder.DecodeData(respMsg.Payload, theItem.OriginalSymbol, new CompositeDecoder());
                        Snapshotlist.Add(theItem.OriginalSymbol, objData);
                        callback.OnCompositeSnapshot(objData);
                    }
                    //         CompositeData objData = (CompositeData)decoder.DecodeData(respMsg.Payload, theItem.OriginalSymbol, new CompositeDecoder());
                    //        callback.OnCompositeSnapshot(objData);
                 
                       
                }
           
            }
            if ((respMsg.HintMask & RespMsg.HintMaskFlag.RespStatus) != 0)
            {

                RespStatus status = respMsg.RespStatus;
                if (status.StatusCode == 1)
                {

                    theItem = FindItemFromMap(respMsg.AttribInfo.Name, itemHandle);
               
                    RemoveItemFromMap(evnt.Handle);
                    callback.OnServerError(CompositeServerError.Composite_Info_Error, theItem.OriginalSymbol);
                    return ;
                       
                }
            }
        }
        private void RemoveItemFromMap(long itemHandle)
        {
            string OrigSymbol = string.Empty;
           foreach (string key in itemList.Keys)
            {
                if (itemList[key].ItemHandle == itemHandle)
                {
                    itemList.Remove(key);
                    break;
                }
            }

        

        
        }
   

        private bool SendLoginRequest()
        {
            OMMItemIntSpec ommItemIntSpec = new OMMItemIntSpec();

            ReqMsg reqMsg = new ReqMsg();
            reqMsg.MsgModelType = RDM.MESSAGE_MODEL_TYPES.MMT_LOGIN;
            reqMsg.InteractionType = ReqMsg.InteractionTypeFlag.InitialImage | ReqMsg.InteractionTypeFlag.InterestAfterRefresh;

            AttribInfo attribInfo = new AttribInfo();
            attribInfo.NameType = Login.USER_ID_TYPES.USER_NAME;
            attribInfo.Name = confgVariables.UserName;

            ElementList elementList = new ElementList();
            ElementEntry element = new ElementEntry();
            DataBuffer elementData = new DataBuffer();
            ElementListWriteIterator elwiter = new ElementListWriteIterator();
            elwiter.Start(elementList);

            element.Name = Login.ENAME_APP_ID;
            elementData.SetFromString(confgVariables.AppId, DataBuffer.DataBufferEnum.StringAscii);
            element.Data = elementData;
            elwiter.Bind(element);

            element.Name = Login.ENAME_POSITION;
            elementData.SetFromString(confgVariables.Position, DataBuffer.DataBufferEnum.StringAscii);
            element.Data = elementData;
            elwiter.Bind(element);
            elwiter.Complete();
            attribInfo.Attrib = elementList;
            reqMsg.AttribInfo = attribInfo;
            ommItemIntSpec.Msg = reqMsg;
            loginHandle = ommConsumer.RegisterClient(null, ommItemIntSpec, this, null);
            if (loginHandle == 0)
            {
                return false;
            }

            return true;
        }
        ItemInfo FindItemFromMap(RFA_String itemName, long itemHandle)
        {
            ItemInfo itmInfo = null;
            foreach (string key in itemList.Keys)
            {
                itmInfo = itemList[key];
                if (itmInfo.ItemHandle == itemHandle)
                {
                    return itmInfo;
                }
            }
            
            if (itemName != (RFA_String)null)
            {
            
            if (itemList.ContainsKey(itemName.ToString())) {
                itmInfo = itemList[itemName.ToString()];

                if ((itmInfo.ItemHandle == 0))
                    itmInfo.ItemHandle = itemHandle;   
                        return itmInfo;
                     }
                 return itmInfo;          
            }
                
            return null;
        }

        List<ItemInfo> FindItemFromMap(RFA_String itemName)
        {
            List<ItemInfo> itemsToBeRemoved = new List<ItemInfo>();
            ItemInfo itmInfo = null;
            string OriginalSymbol = itemName.ToString();
           
            foreach (string key in itemList.Keys) {
                if (OriginalSymbol == itemList[key].OriginalSymbol) {
                    if (!itemsToBeRemoved.Contains(itemList[key])) {
                        itemsToBeRemoved.Add(itemList[key]);
                    }
                }
            }
            return itemsToBeRemoved;
        }



    }
}
