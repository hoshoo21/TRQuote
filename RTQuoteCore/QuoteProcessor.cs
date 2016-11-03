using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TRQuoteCore.QuoteStructure;
namespace TRQuoteCore.Core
{
    public class QuoteProcessor : IQuoteProcessor
    {
        static List<QuoteProcessor> allQuoteProcessors = new List<QuoteProcessor>();

        private CoreContentGateway gateway;
        public QuoteProcessor(IQuoteProcessor quoteProc)
            : this()
        {
            _quoteProc = quoteProc;
        }
      
        public QuoteProcessor()
        {
            lock (allQuoteProcessors)
            {
                allQuoteProcessors.Add(this);
            }
            gateway = new CoreContentGateway(this);
        }
        public void SetCallback(IQuoteProcessor quoteProc)
        {
            _quoteProc = quoteProc;
        }
      
        private IQuoteProcessor _quoteProc;
        #region Status Reporting
        void IQuoteProcessor.OnConnectionStatus(ConnectionStatus status)
        {
            this.OnConnectionStatus(status);
        }

        protected virtual void OnConnectionStatus(ConnectionStatus status)
        {
            if (_quoteProc != null) { }
                _quoteProc.OnConnectionStatus(status);
        }
        void IQuoteProcessor.OnServerError(CompositeServerError error, string Symbol)
        {
            this.OnServerError(error, Symbol);
        }
        protected virtual void OnServerError(CompositeServerError error, string Symbol)
        {
            if (_quoteProc != null)
                _quoteProc.OnServerError(error, Symbol);
        }
        #endregion
        #region SnapsotData
        void IQuoteProcessor.OnCompositeSnapshot(CompositeData composite)
        {
            this.OnCompositeSnapshot(composite);
        }
        protected virtual void OnCompositeSnapshot(CompositeData composite)
        {
            if (_quoteProc != null)
                _quoteProc.OnCompositeSnapshot(composite);
        }
        #endregion
        #region IQuoteMembers
        void IQuoteProcessor.OnTrade(TradeData trade)
        {
            this.OnTrade(trade);
        }
        protected virtual void OnTrade(TradeData trade)
        {
            if (_quoteProc != null)
                _quoteProc.OnTrade(trade);
        }
        void IQuoteProcessor.OnBBOQuote(BBOQuoteData bboQuote)
        {
            this.OnBBOQuote(bboQuote);
        }
        protected virtual void OnBBOQuote(BBOQuoteData bboQuote)
        {
            if (_quoteProc != null)
                _quoteProc.OnBBOQuote(bboQuote);
        }

        #endregion
        public void RequestQmonData(string Symbol)
        {
            gateway.RequestLevel2Data(Symbol);
        }

        public void UnsubscribeSymbol(string Symbol)
        {
            gateway.UnsubscribeSymbol(Symbol);
        }
        public void UnsubscribeAll()
        {
            gateway.UnsubscribeAll();
        }

        public string getPrimaryExchange(ushort ExchangeValue) {
            return gateway.getPrimaryExchange(ExchangeValue);
        }
        public string getTradeExchange(ushort ExchangeValue ) {
            return gateway.getTradingExchange(ExchangeValue);
        }

        public void Shutdown()
        {
            lock (allQuoteProcessors)
            {
                allQuoteProcessors.Remove(this);
            }
            gateway.Shutdown();
        }
       
    }
}
