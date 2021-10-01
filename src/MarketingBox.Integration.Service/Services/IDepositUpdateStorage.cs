using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using MarketingBox.Integration.Service.Messages.Deposits;

namespace MarketingBox.Integration.Service.Services
{
    public interface IDepositUpdateStorage
    {
        void Add(string requestLeadUniqueId, DepositUpdateMessage depositUpdateMessage);
        List<KeyValuePair<string, DepositUpdateMessage>> GetAll();
        void Remove(string requestLeadUniqueId);
    }

    public class DepositUpdateStorage : IDepositUpdateStorage
    {
        private ConcurrentDictionary<string, DepositUpdateMessage> _data = new ConcurrentDictionary<string, DepositUpdateMessage>();
        public void Add(string requestLeadUniqueId, DepositUpdateMessage depositUpdateMessage)
        {
            _data[requestLeadUniqueId] = depositUpdateMessage;
        }

        public List<KeyValuePair<string, DepositUpdateMessage>> GetAll()
        {
            return _data.ToList();
        }

        public void Remove(string requestLeadUniqueId)
        {
            _data.TryRemove(requestLeadUniqueId, out _);
        }

    }
}