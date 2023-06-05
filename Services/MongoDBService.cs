using unwallet.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;

namespace unwallet.Services;

public class MongoDBService{

    private readonly IMongoCollection<ScheduledPayment>? _scheduledPaymentCollection;

    public MongoDBService(IOptions<MongoDBSettings> mongoDBSettings){
        MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionURI);
        IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
        _scheduledPaymentCollection = database.GetCollection<ScheduledPayment>(mongoDBSettings.Value.CollectionName);
    }

    public async Task<List<ScheduledPayment>> GetAsync()
    {
        return await _scheduledPaymentCollection.Find(new BsonDocument()).ToListAsync();
    }

    public async Task<ScheduledPayment> GetByIdAsync(string id)
    {
        var filter = Builders<ScheduledPayment>.Filter.Eq( m => m._id, id);
        return await _scheduledPaymentCollection.Find(filter).FirstOrDefaultAsync();
    }    

    public async Task<ScheduledPayment> CreateAsync(ScheduledPayment scheduledPayment){
        //var document = scheduledPayment.ToBsonDocument();
        //var convertedDoc = BsonSerializer.Deserialize<ScheduledPayment>(document);
        //if(_scheduledPaymentCollection is not null) await _scheduledPaymentCollection.InsertOneAsync(convertedDoc);
        if(_scheduledPaymentCollection is not null) await _scheduledPaymentCollection.InsertOneAsync(scheduledPayment);
        return scheduledPayment;
    }

    public async Task UpdateAsync(string id, ScheduledPayment scheduledPayment){
        scheduledPayment._id = id;

        await _scheduledPaymentCollection.ReplaceOneAsync(
            sPayment => sPayment._id == id,
            scheduledPayment,
            new ReplaceOptions { IsUpsert = false}
        );
    }

    public async Task<bool> DeleteAsync(string id){
        var deleteResult = await _scheduledPaymentCollection.DeleteOneAsync(
            Builders<ScheduledPayment>.Filter.Eq(sPayment => sPayment._id, id)
        );
        return deleteResult.DeletedCount > 0;
    }

    public async Task<List<ScheduledPayment>> GetListFromIntegerAsync(int n){
        var filter = Builders<ScheduledPayment>.Filter.Eq("user_id", n);
        var payments = await _scheduledPaymentCollection.Find(filter).ToListAsync();
        return payments;
    }

}