using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API_SQL.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CardSaleTransactions : ControllerBase
    {
        public int CompanyNo { get; set; }
        public string Password { get; set; }

        // GET: /LatestCompanyTraceNo
        /// <summary>
        /// Returns the latest CompanyTraceNo from CardSaleTransactions.
        /// </summary>
        /// <remarks>..../CardSaleTransactions/LatestCompanyTraceNo?CompanyNo=123&amp;Password=123<br></br>**If the user doesn't exist, a new user will be created!**</remarks> 
        /// <param name="Password">The Password must be at least 8 characters long</param>
        [HttpGet("LatestCompanyTraceNo")]
        public int Get1([Required] int CompanyNo, [Required] string Password)
        {
            int result = -1;

            DBConnect db = new DBConnect();
            int userID = db.SQLGetUser(CompanyNo, Password);
            if(userID != -1 )
            {
                Console.WriteLine("the user was found "+userID);
                result = userID;
            }
            else
            {
                Console.WriteLine("A user will be created");
                int newUserID = db.SQLNewUser(CompanyNo, Password);
                Console.WriteLine("New user with ID "+newUserID);
                result = newUserID;
            }
            db.DeleteUserNotActive(); 
            return db.SQLLatest(result);
   
        }

        // GET CardsaleTransActions/
        /// <summary>
        /// Returns all data from CardSaleTransactions
        /// </summary>
        /// <remarks>..../CardSaleTransactions?CompanyNo=123&amp;Password=123</remarks> 
        [HttpGet]
        public List<Models.CardSaleTransactions>? Get2([Required]int CompanyNo, [Required] string Password) //ActionResult<CardSaleTransactions>
        {
            DBConnect db = new DBConnect();
            int userID = db.SQLGetUser(CompanyNo, Password);
            string table = db.GetTheUsersTable(userID);

            List<Models.CardSaleTransactions> cardSaleTransactions = db.SQLGetCardSaleTransactions(table);
            return cardSaleTransactions;
        }

        // POST CardsaleTransActions/
        /// <summary>
        /// Inserts one line of CardSaleTransactions
        /// </summary>
        /// <remarks>..../CardSaleTransactions?CompanyNo=123&amp;Password=123</remarks> 
        [HttpPost]
        public void Post([Required] int CompanyNo, [Required] string Password,[FromBody] Models.CardSaleTransactions value)
        {
        
            DBConnect db = new DBConnect();
            int userID = db.SQLGetUser(CompanyNo, Password);
            string table = db.GetTheUsersTable(userID);

            db.INSERTCardSaleTransactions( table, value);
        }

    }


}
