using System;
using System.Linq; //allows us to access query methods(FirstOrDefault)
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;//allow us to use include
using BeltExam.Models;

namespace BeltExam.Controllers
{
    public class HomeController : Controller
    {
        private readonly MyContext _context;

        public HomeController(MyContext myContext)
        {
            _context = myContext;
        }


        [HttpGet("home")]
        public IActionResult Dashboard()
        {
            //get the UserId from session and store in userId
            int? userId = HttpContext.Session.GetInt32("UserId"); //? meaning we are not sure if there is a userId in session

            if (userId == null)
            {
                //no user present
                return RedirectToAction("LoginReg", "Users");
            }

            //put the userId in ViewBag to use in cshtml
            ViewBag.User = _context
                .Users
                .Find(userId);

            //extract all the activities from ViewBag to display in cshtml
            ViewBag.Activity = _context
                .Activities
                .Include(activity => activity.PlanedBy)
                .Include(activity => activity.Participants)
                .OrderBy(activity => activity.Date)
                .ToList();

            return View();
        }

        [HttpGet("activity/new")]
        public IActionResult NewActivityPage()
        {
            int? userId = HttpContext.Session.GetInt32("UserId"); //get the logged in user

            if (userId == null)
            {
                return RedirectToAction("LoginReg", "Users"); //action: go to LoginReg page, controller: UsersController//
            }

            ViewBag.User = _context
                .Users
                .Find(userId);

            return View();
        }

        //CreateActivity
        [HttpPost("activities")]
        public IActionResult CreateActivity(Activity activityToCreate)//take the activity form into and name it activityToCreate
        {

            //validate the date to make sure it is in the future
            if (activityToCreate.Date < DateTime.Now)
            {
                ModelState.AddModelError("Date", "You must specity a date in the future");
            }

            //if there is false info in the form, direct to the form page
            if (ModelState.IsValid)
            {

                //if the form is valid,get planed user in session
                //the form created all the necessary info except UserId, and we need to save the UserId from Session
                activityToCreate.UserId = HttpContext.Session.GetInt32("UserId").GetValueOrDefault();

                //add and save changes to the DB
                _context.Add(activityToCreate);
                _context.SaveChanges();

                // after we add the activityToCreate in the _context and save the change, then the activityId will get created
                // to send to the Activity page
                return Redirect($"/activities/{activityToCreate.ActivityId}");
            }

            int userId = HttpContext.Session.GetInt32("UserId").GetValueOrDefault();

            ViewBag.User = _context
                .Users
                .Find(userId);

            return View("NewActivityPage");

        }

        //After successful create the activity, the activity detail goes to /activities/{activityToCreate.activityId}
        [HttpGet("activities/{id}")]
        public IActionResult ActivityPage(int id)
        {
            //get the activity to display
            ViewBag.activity = _context
                .Activities
                //one to many relationship: each activity I find, I want to return the movie that is PlanedBy
                .Include(activity => activity.PlanedBy)
                //populate the Rsvps first
                .Include(activity => activity.Participants)
                    //and then populate the users
                    .ThenInclude(activity => activity.User)
                .FirstOrDefault(activity => activity.ActivityId == id); //take the activity, whose activityId == id

            ViewBag.User = _context
                .Users
                .Find(HttpContext.Session.GetInt32("UserId"));

            //go to the activity display page
            return View();
        }

        // User in session add a going to the activity
        [HttpPost("activities/{id}/going")]
        public IActionResult AddParticipant(int id)
        {
            var participantToAdd = new Participant();

            participantToAdd.UserId = HttpContext.Session.GetInt32("UserId").GetValueOrDefault(); // add UserId
            participantToAdd.ActivityId = id;

            _context.Add(participantToAdd);
            _context.SaveChanges();

            return RedirectToAction("Dashboard");
        }
        
        // User in session UN-Going to the activity
        [HttpPost("activities/{id}/going/delete")]
        public IActionResult RemoveParticipant(int id)
        {
            //get the loggedin userId
            var userId = HttpContext.Session.GetInt32("UserId").GetValueOrDefault();
            //get the P that need to remove
            var participantToRemove = _context
                .Participants
            //get the first like where the activity id matches the id we took in the form, and the user id in session matches the user id
                .FirstOrDefault(participant => participant.ActivityId == id && participant.UserId == userId);

            _context.Participants.Remove(participantToRemove);
            _context.SaveChanges();

            return RedirectToAction("Dashboard");
        }

         // User who posted can delete
        [HttpPost("activities/{id}/delete")]
        public IActionResult DeleteActivity (int id)
        {
            //get the wedding that need to remove
            var activityToDelete = _context
                .Activities
            //get the first like where the activity id matches the id we took in the form, and the user id in session matches the user id
                .Find(id);

            _context.Activities.Remove(activityToDelete);
            _context.SaveChanges();

            return RedirectToAction("Dashboard");
        }

    }
}
