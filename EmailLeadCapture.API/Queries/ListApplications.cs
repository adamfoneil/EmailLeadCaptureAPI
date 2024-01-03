using Dapper.QX;
using EmailLeadCapture.Database;

namespace EmailLeadCapture.API.Queries;

public class ListApplications : Query<Application>
{
	public ListApplications() : base("SELECT * FROM public.application ORDER BY \"Name\"")
	{            
	}
}
