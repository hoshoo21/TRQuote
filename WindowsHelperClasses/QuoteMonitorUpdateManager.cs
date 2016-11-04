using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TRQuoteCore.QuoteStructure;
using TRQuoteCore.Core;
namespace TRQuoteCore.WindowsHelperClasses
{
    public interface IQuoteMonitorUpdateManager
    {
        Dictionary<string, QuoteMonitorUpdateData> quoteUpdates
        {
            get;
        }
        void RequestAll();
        void UnRegisterAll();
        void AddSymbol(string symbol);
        void RemoveSymbol(string symbol);
        void Disposed();
    }
    public class QuoteMonitorUpdateManager : IQuoteMonitorUpdateManager
    {
        public Dictionary<string, QuoteMonitorUpdateData> _quoteUpdates;

        public bool IsMarketOpen
        {
            get
            {
                DateTime opTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 9, 30, 0);

                return (DateTime.Now > opTime);//(GetESTTime() > opTime);

            }



        }
        public DateTime GetESTTime()
        {

            DateTime stime = new DateTime(DateTime.Now.Year, 3, 1, 2, 0, 0);
            stime = stime.AddDays(7);
            stime = stime.AddDays((int)stime.DayOfWeek);


            DateTime etime = new DateTime(DateTime.Now.Year, 11, 1, 2, 0, 0);
            etime = etime.AddDays(7);
            etime = etime.AddDays((int)stime.DayOfWeek);

            bool dst = false;

            if (DateTime.Now > stime && DateTime.Now < etime)
            {
                dst = true;
            }

            TimeZone localZone = TimeZone.CurrentTimeZone;
            DateTime mytime = DateTime.Now;

            System.TimeSpan utcOffset = localZone.GetUtcOffset(mytime);
            DateTime estTime = mytime.AddHours((utcOffset.Hours + 5) * (-1));

            if (dst)
            {
                estTime = estTime.AddHours(1);
            }

            return estTime;


        }


        /// <summary>
        /// Call this after a disconnect to get comp. and realtime streams for all sybmols.
        /// </summary>
        public void RequestAll()
        {
            try
            {
                string[] symbollist = new string[0];
                lock (feedSyncLock)
                {
                    symbollist = new string[_quoteUpdates.Keys.Count];
                    _quoteUpdates.Keys.CopyTo(symbollist, 0);
                }
                foreach (string symbol in symbollist)
                {
                    _Instance.RequestQmonData(symbol);
                }
            }
            catch (Exception exx)
            {
               // _Instance.LogMessage("OMS : Got error at RequestAll: " + exx.Message);
            }
        }
        TRQuoteCore.Core.QuoteProcessor  _Instance;
        //public QuoteMonitorUpdateManager(QuoteProcessor Instance)
        public QuoteMonitorUpdateManager(QuoteProcessor Instance)
        {
            _quoteUpdates = new Dictionary<string, QuoteMonitorUpdateData>();
            _Instance = Instance;
        }

        public void AddSymbol(string symbol)
        {

            string tempSymbol = symbol;
           
            lock (feedSyncLock)
            {
                if (!_quoteUpdates.ContainsKey(tempSymbol))
                {

                    _quoteUpdates.Add(tempSymbol, new QuoteMonitorUpdateData());
                    _Instance.RequestQmonData(symbol);

                }
                else
                {
                    _quoteUpdates[tempSymbol].Count++;
                    _Instance.RequestQmonData(symbol);
                }
            }
        }

        public void RemoveSymbol(string symbol)
        {
            bool containsSymbol = false;
            lock (feedSyncLock)
            {
                if (_quoteUpdates.ContainsKey(symbol))
                {
                    _quoteUpdates[symbol].Count--;
                     containsSymbol = true;
                     _quoteUpdates.Remove(symbol);
                   
                }
            }
            if (containsSymbol)
            {
                _Instance.UnsubscribeSymbol(symbol);
            }
        }


        public void UnRegisterAll()
        {
            string[] symbollist = new string[0];
            lock (feedSyncLock)
            {
                symbollist = new string[_quoteUpdates.Keys.Count];
                _quoteUpdates.Keys.CopyTo(symbollist, 0);
            }
            _Instance.UnsubscribeAll();
        }

        public void Update(TradeData trade)
        {
            lock (feedSyncLock)
            {
                if (_quoteUpdates.ContainsKey(trade.Symbol))
                {
                    if (!double.IsNaN(trade.Last))
                    {
                        _quoteUpdates[trade.Symbol].Last = trade.Last;
                    }

                    if (trade.Price != double.NaN)
                    {
                        if ((trade.PriceFlags & PriceFlags.Set_Open) == PriceFlags.Set_Open)
                        {
                            _quoteUpdates[trade.Symbol].Open = trade.Price;
                        }

                        if ((trade.PriceFlags & PriceFlags.Set_High) == PriceFlags.Set_High)
                        {
                            _quoteUpdates[trade.Symbol].High = trade.Price;
                        }

                        if ((trade.PriceFlags & PriceFlags.Set_Low) ==PriceFlags.Set_Low)
                        {
                            _quoteUpdates[trade.Symbol].Low = trade.Price;
                        }
                        if ((trade.PriceFlags & PriceFlags.Set_Close) == PriceFlags.Set_Close)
                        {
                            _quoteUpdates[trade.Symbol].Close = trade.Price;
                        }


                        //if ((trade.PriceFlags & GQCore.PriceFlags.Set_Last) == GQCore.PriceFlags.Set_Last)
                        //{
                        //    _quoteUpdates[trade.Symbol].Last = trade.Price;
                        //}
                    }

                    double percentChange = 0;
                    //try
                    //{
                    //    if (trade.Netchange != double.NaN)
                    //    {
                    //        double lastClose = _quoteUpdates[trade.Symbol].Close;
                    //        percentChange = (trade.Netchange / lastClose) * 100;
                    //    }
                    //}
                    //catch
                    //{
                    //    //Any Maths Exception.
                    //}

                    _quoteUpdates[trade.Symbol].PrecentChange = trade.PercentChange;


                    if (IsMarketOpen
                         || trade.Symbol.Contains("/")
                        || trade.Symbol.Contains(".OS")
                        || trade.Symbol.Contains(".OL")
                        || trade.Symbol.Contains(".ASX"))
                    {
                        if (trade.Netchange != double.NaN)
                        {
                            _quoteUpdates[trade.Symbol].Change = trade.Netchange;
                        }
                    }
                    else
                    {

                        if (trade.Last == 0)
                        {
                            if (trade.Netchange != double.NaN)
                            {
                                _quoteUpdates[trade.Symbol].Change = trade.Netchange;
                            }
                        }
                        else
                        {
                            _quoteUpdates[trade.Symbol].Change = trade.Last - _quoteUpdates[trade.Symbol].PreviousClose;
                        }
                    }

                    //if (IsMarketOpen)
                    //{
                    //    _quoteUpdates[trade.Symbol].Change = trade.Last - _quoteUpdates[trade.Symbol].Open;

                    //}
                    //else
                    //{
                    //    _quoteUpdates[trade.Symbol].Change = trade.Last - _quoteUpdates[trade.Symbol].PreviousClose;


                    //}




                    _quoteUpdates[trade.Symbol].Volume = trade.TotalVolume;
                    if (trade.TradeTime > new DateTime(2000, 1, 1))
                    {
                        _quoteUpdates[trade.Symbol].TradeTime = trade.TradeTime;
                    }
                    if (trade.ReportingExg != 69)
                    {
                       _quoteUpdates[trade.Symbol].Exchange = trade.ReportingExg ;
                    }
                    _quoteUpdates[trade.Symbol].VWAP = trade.VWAP;
                    _quoteUpdates[trade.Symbol].MktCap = trade.MarketCapt;
                    // _quoteUpdates[trade.Symbol].MktCap = trade.Last * 1000 * _quoteUpdates[trade.Symbol].SharesOutstanding;



                }
            }
        }


        public void Update(BBOQuoteData quote)
        {
            lock (feedSyncLock)
            {
                if (_quoteUpdates.ContainsKey(quote.Symbol))
                {
                    if (quote.Ask != 0)
                    {
                        _quoteUpdates[quote.Symbol].Ask = quote.Ask;
                        _quoteUpdates[quote.Symbol].AskSize = quote.Asksize;
                        _quoteUpdates[quote.Symbol].AskTime = quote.QuoteTime;
                    }
                    if (quote.Bid != 0)
                    {
                        _quoteUpdates[quote.Symbol].Bid = quote.Bid;
                        _quoteUpdates[quote.Symbol].BidSize = quote.Bidsize;
                        _quoteUpdates[quote.Symbol].BidTime = quote.QuoteTime;
                    }

                    _quoteUpdates[quote.Symbol].CurrentMidMkt = (_quoteUpdates[quote.Symbol].Bid + _quoteUpdates[quote.Symbol].Ask) / 2;
                }
            }
        }

        public bool IsOptionsymbol(CompositeData data)
        {

            string SymbolCheck = data.Symbol;

            return SymbolCheck.Trim().StartsWith(".");

        }
        public static object feedSyncLock = new object();


        public void Update(CompositeData data)
        {
            lock (feedSyncLock)
            {
                if (_quoteUpdates.ContainsKey(data.Symbol))
                {
                    if (IsMarketOpen)
                    {
                        _quoteUpdates[data.Symbol].Ask = data.Ask;
                        _quoteUpdates[data.Symbol].Bid = data.Bid;
                        _quoteUpdates[data.Symbol].CurrentMidMkt = (data.Ask + data.Bid) / 2;

                    }
                    else
                    {
                        if (IsOptionsymbol(data))
                        {
                            _quoteUpdates[data.Symbol].Ask = data.PrevAsk;
                            _quoteUpdates[data.Symbol].Bid = data.PrevBid;
                            _quoteUpdates[data.Symbol].CurrentMidMkt = (data.PrevAsk + data.PrevBid) / 2;
                        }
                        else
                        {
                            _quoteUpdates[data.Symbol].Ask = data.Ask;
                            _quoteUpdates[data.Symbol].Bid = data.Bid;
                            _quoteUpdates[data.Symbol].CurrentMidMkt = (data.Ask + data.Bid) / 2;
                        }

                    }

                    _quoteUpdates[data.Symbol].BidTime = data.BidTime;
                    _quoteUpdates[data.Symbol].AskTime = data.AskTime;

                    _quoteUpdates[data.Symbol].AskSize = data.Asksize;

                    _quoteUpdates[data.Symbol].BidSize = data.Bidsize;

                    if (IsMarketOpen
                        || data.Symbol.Contains("/")
                        || data.Symbol.Contains(".OS")
                        || data.Symbol.Contains(".OL")
                        || data.Symbol.Contains(".ASX"))
                    {
                        if (data.PriceNetchange != double.NaN)
                        {
                            _quoteUpdates[data.Symbol].Change = data.PriceNetchange;
                        }
                    }
                    else
                    {
                        if (data.Last == 0)
                        {
                            _quoteUpdates[data.Symbol].Change = data.PriceNetchange;
                        }
                        else
                        {
                            _quoteUpdates[data.Symbol].Change = data.Last - data.PrevClose;
                        }

                    }

                    if (IsMarketOpen)
                    {
                        _quoteUpdates[data.Symbol].Change = data.Last - data.Open;
                    }
                    else
                    {
                        _quoteUpdates[data.Symbol].Change = data.Last - data.PrevClose;
                    }

                    _quoteUpdates[data.Symbol].High = data.High;


                    if (data.Last == 0)
                    {
                        _quoteUpdates[data.Symbol].Last = data.PrevClose;
                    }
                    else
                    {
                        _quoteUpdates[data.Symbol].Last = data.Last;
                    }
                    _quoteUpdates[data.Symbol].AnnualDividend = data.AnnualDivAmount;
                    _quoteUpdates[data.Symbol].Low = data.Low;
                    _quoteUpdates[data.Symbol].Open = data.Open;
                    _quoteUpdates[data.Symbol].Volume = data.Volume;
                    _quoteUpdates[data.Symbol].Close = data.PrevClose;
                    _quoteUpdates[data.Symbol].TradeTime = data.TradeTime;
                   // _quoteUpdates[data.Symbol].Volatility = data.Volatility;
                    _quoteUpdates[data.Symbol].AvgPx30d = data.AvgPx30d;
                    //_quoteUpdates[data.Symbol].ADV30d = data.AvgVolume30d;
                    _quoteUpdates[data.Symbol].DaysTillExpiry = data.OptionDaysTillExpire;
                    _quoteUpdates[data.Symbol].Strike = data.OptionStrikePrice;
                    _quoteUpdates[data.Symbol].Volatility = data.Volatility;
                    _quoteUpdates[data.Symbol].VWAP = data.VWAP;
                    _quoteUpdates[data.Symbol].PreviousClose = data.PrevClose;
                    _quoteUpdates[data.Symbol].Symbol = data.Symbol;
                    _quoteUpdates[data.Symbol].Wk52h = data.High52;
                    _quoteUpdates[data.Symbol].Wk52l = data.Low52;
                    _quoteUpdates[data.Symbol].Description = data.Description;
                     _quoteUpdates[data.Symbol].PExchgShort = _Instance.getPrimaryExchange(data.Exchange);
                    _quoteUpdates[data.Symbol].Div = data.LastDivAmount;
                    _quoteUpdates[data.Symbol].MktCap = data.SharesOutstanding * data.Last * 1000;
                    _quoteUpdates[data.Symbol].SharesOutstanding = data.SharesOutstanding;










                    double percentChange = 0;
                    try
                    {
                        if (data.Netchange != double.NaN)
                        {
                            percentChange = (data.Netchange / data.PrevClose) * 100;
//                            _quoteUpdates[data.Symbol].PrecentChange = percentChange;
                      
                        }

                    }


                    catch
                    {
                        //Any Maths Exception.
                    }
                    _quoteUpdates[data.Symbol].PrecentChange = data.PercentChange;
                      
                }
            }
        }
        public void Disposed()
        {
        }

        public Dictionary<string, QuoteMonitorUpdateData> quoteUpdates
        {
            get { return _quoteUpdates; }
        }


    }
}
