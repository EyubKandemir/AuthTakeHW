using Business.Messages;
using Data.Results;

namespace Business.ValidationRules.Site
{
    public class SiteAddValidator
    {
        public Result Run(Data.Tables.Sites site)
        {
            if (string.IsNullOrEmpty(site.SiteName))
                return new ErrorResult(SitesMessages.SiteNameMandatory);

            if (string.IsNullOrEmpty(site.Password))
                return new ErrorResult(SitesMessages.SitePasswordMandatory);

            return new SuccessResult();
        }
    }
}
