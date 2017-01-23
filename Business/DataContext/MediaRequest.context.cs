using System.Collections.Generic;
using System.Linq;

namespace Business
{
    public interface IMediaRequestContext
    {
        List<MediaRequest> GetAll();
        MediaRequest GetByID(int id);
        List<MediaRequest> GetMultipleByUserName(string userName);
        bool Save(MediaRequest request);
    }

    public class MediaRequestContext : BaseContext, IMediaRequestContext
    {
        public List<MediaRequest> GetAll()
        {
            return DB.Select<MediaRequest>(DBTable.media);
        }
        public MediaRequest GetByID(int id)
        {
            List<MediaRequest> results = DB.Select<MediaRequest>(DBTable.media, "MediaID = " + id, 1);
            if (results != null && results.Any())
            {
                return results[0];
            }
            return null;
        }

        public List<MediaRequest> GetMultipleByUserName(string userName)
        {
            List<MediaRequest> results = DB.Select<MediaRequest>(DBTable.media, "SenderID = '" + userName + "'");
            return results;
        }


        public bool Save(MediaRequest request)
        {
            DB.Update(DBTable.media, BuildDictionary(), new KeyValuePair<string, string>("MediaID", request.MediaID.ToString()));
            return true;
        }

    }
}
