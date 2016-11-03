using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TRQuoteCore.QuoteStructure;

namespace TRQuoteCore.Core
{
    public interface IQuoteProcessor
    {
       // void OnImbalance(ImbalanceData trade);
        void OnTrade(TradeData trade);
       // void OnMMQuote(MMQuoteData mmQuote);
        void OnBBOQuote(BBOQuoteData bboQuote);
       // void OnQuote(QuoteData quote);
      //  void OnOptionChainSnapshot(OptionChainData optionChain);
        void OnCompositeSnapshot(CompositeData composite);
      //  void OnMMDepthSnapshot(Level2Data mmDepth);
      //  void OnRegionalSnapshot(MontageData regional);
        void OnConnectionStatus(ConnectionStatus status);
        void OnServerError(CompositeServerError error, string Symbol);
    }
    public enum CompositeServerError
    {
        Composite_Info_Error = 101,
        Level2_Info_Error = 102,
        Montage_Info_Error = 103,
        Option_Chain_Error = 104,
        Composite_Limit_Error = 105,
        Level2_Limit_Error = 106,
        Montage_Limit_Error = 107,
        Chain_Limit_Error = 108,
        Underlying_Symbol_Info_Error = 109,
        Composite_Info_Busy = 201,
        Level2_info_Busy = 202,
        Montage_Info_Busy = 203,
        Chain_Info_Busy = 204,
        Composite_Info_Feed_Down = 205,
        Level2_Feed_Down = 206,
        Montage_Feed_Down = 207,
        Chain_Feed_Down = 208,
        Underlying_Feed_Down = 209,
        Underlying_Info_Busy = 210,
    }
    public enum ConnectionStatus
    {
        Ready = 1,
        Disconnected = 2,
        Bad_Login = 3,
        Bad_User = 4,
        Duplicate_User_Not_Allowed = 5,
        Bad_Product_ID = 6,
        Account_Expired = 7,
        Connected = 11,
        Connection_Error = 12,
        Slave_Retry_Connection = 20,
        Slave_Reconnect = 21,
        Busy = 122,
        Timeout = 123,
        Bad_Port = 124,
        Bad_IPAddress = 125,
        Host_Unreachable = 126,
        Host_Not_Found = 127,
        Connection_Refused = 128,
        Access_Denied = 129,
        Server_Full = 130
    }
}
