using Business.Messages;
using Data.Results;

namespace Business.ValidationRules.Site
{
    public class SiteUpdateValidator
    {
        public Result Run(Data.Tables.Sites site)
        {
            if (site.Id <= 0)
                return new ErrorResult(SitesMessages.IdCantBeZeroOrNull);

            if (string.IsNullOrEmpty(site.SiteName))
                return new ErrorResult(SitesMessages.SiteNameMandatory);

            if (string.IsNullOrEmpty(site.Password))
                return new ErrorResult(SitesMessages.SitePasswordMandatory);

            return new SuccessResult();
        }
    }
}
