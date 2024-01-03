using Dapper.QX;
using EmailLeadCapture.Database;
using ProductIdeas.Database;

namespace ProductIdeas.Queries;

public class ListApplications : Query<Application>
{
	public ListApplications() : base("SELECT * FROM public.application ORDER BY \"Name\"")
	{            
	}
}
