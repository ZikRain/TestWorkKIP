using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using TestWorkKip.Entities;
using TestWorkKip.ORM;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace TestWorkKip.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ReportController : ControllerBase
    {

        private readonly ILogger<ReportController> _logger;
        protected readonly ApplicationContext _ApplicationContext;
        protected readonly IConfiguration _IConfiguration;
        public ReportController(ILogger<ReportController> logger, ApplicationContext __ApplicationContext, IConfiguration __IConfiguration)
        {
            _logger = logger; _ApplicationContext = __ApplicationContext; _IConfiguration = __IConfiguration;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> user_statistics([Required()] string user_guid, [Required()] DateTime start_dateTime, [Required()] DateTime end_dateTime)
        {
            var guid = Guid.NewGuid().ToString();

            await _ApplicationContext.Queries.AddAsync(new Entities.Query()
            {
                Query_guid = guid,
                StarDateTime = start_dateTime,
                EndDateTime = end_dateTime,
                User_guid = user_guid,
                CreateDateTime = DateTime.Now,
                Percent = 0
            });

            var rnd = new Random();
            await _ApplicationContext.Results.AddAsync(new Result()
            {
                Count_sign_in = rnd.Next(),
                Query_guid = guid,
                user_guid = user_guid
            });

            await _ApplicationContext.SaveChangesAsync();

            return Ok(guid);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> info([Required()] string query_guid)
        {

            var query = await _ApplicationContext.Queries.FirstOrDefaultAsync(x=>x.Query_guid==query_guid);

            if(query == null) return  NotFound();

            var result = await _ApplicationContext.Results.FirstOrDefaultAsync(x => x.Query_guid == query_guid);

            if (query.Percent == 100)
            {
                return Ok(new ReturnQuery() { query = query_guid ,
                percent = 100,
                result = new ReturnResult()
                {
                    count_sign_in = result.Count_sign_in,
                    user_id = result.user_guid
                }
                });
            }

            var interval = Convert.ToInt64(_IConfiguration.GetSection("TestConstant").GetSection("Time").Value);

            var percent = (DateTime.Now - query.CreateDateTime).TotalMilliseconds*100/interval;

            query.Percent = Convert.ToInt32(percent) > 100 ? 100 : Convert.ToInt32(percent);
            _ApplicationContext.Queries.Update(query);
            await _ApplicationContext.SaveChangesAsync();

            if (percent >= 100)
            {
                
                return Ok(new ReturnQuery()
                {
                    percent = 100,
                    query = query_guid ,
                    result = new ReturnResult()
                    {
                        user_id = result.user_guid,
                        count_sign_in = result.Count_sign_in
                    }
                });
            }
            else
            {
                return Ok(new ReturnQuery()
                {
                    percent = query.Percent,
                    query = query_guid,
                    result = null
                });
            }
           
        }
    }
}