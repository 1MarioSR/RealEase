using RealEase.Web.Data;

namespace RealEase.Web.Controllers
{
    internal class RealEaseController
    {
        private RealEaseDbContext db;

        public RealEaseController(RealEaseDbContext db)
        {
            this.db = db;
        }
    }
}