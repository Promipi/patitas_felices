using patitas_felices.APP;
using patitas_felices.APP.View;

namespace patitas_felices.APP
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute("FeedersPage", (typeof(FeedersPage)));
            Routing.RegisterRoute("FeedersPage/FeederDetailsPage",(typeof(FeederDetailsPage)));
            Routing.RegisterRoute("FeedersPage/FeederDetailsPage/PhotosPage", (typeof(PhotosPage)));
        }
    }
}