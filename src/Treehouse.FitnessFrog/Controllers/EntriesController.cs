using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Treehouse.FitnessFrog.Data;
using Treehouse.FitnessFrog.Models;

namespace Treehouse.FitnessFrog.Controllers
{
    public class EntriesController : Controller
    {
        private EntriesRepository _entriesRepository = null;

        public EntriesController()
        {
            _entriesRepository = new EntriesRepository();
        }

        public ActionResult Index()
        {
            List<Entry> entries = _entriesRepository.GetEntries();

            // Calculate the total activity.
            double totalActivity = entries
                .Where(e => e.Exclude == false)
                .Sum(e => e.Duration);

            // Determine the number of days that have entries.
            int numberOfActiveDays = entries
                .Select(e => e.Date)
                .Distinct()
                .Count();

            ViewBag.TotalActivity = totalActivity;
            ViewBag.AverageDailyActivity = (totalActivity / (double)numberOfActiveDays);

            return View(entries);
        }

        public ActionResult Add()
        {
            return View();
        }
        /*We at times need to handle a form Post. Below is how we can do that.
        We use an attribute below, and give it a parameter value. Thats associted with the method
        We can stack the attributes, or comma separate them
        To test this we can add a breakpoint on add, then run the program*/
        [ActionName("Add"), HttpPost]
        public ActionResult Add(DateTime? date, int? activityId, double? duration, Entry.IntensityLevel? intensity, bool? exclude, string notes)
        {
            /* We can also use a model to capture POST data. We wouldn't want to do it for each form field though. So MVC lets us define
              method params for each one. See Above. Also means we can rechange from AddPost to Add.
            You can now run this, and see at the breakpoint it return the values. This is Model Binding */

            /* We can also write more than strings as well through the model binder. Including enums as well. Now we may have an issue where "what if a
             null is passed in". Well we can get around that by making these types nullable. Which is just a question mark after each type*/

            /* This attempted value handles any improper value input*/
            ViewBag.Date = ModelState["Date"].Value.AttemptedValue;
            ViewBag.ActivityId = ModelState["ActivityId"].Value.AttemptedValue;
            ViewBag.Duration = ModelState["Duration"].Value.AttemptedValue;
            ViewBag.Intensity = ModelState["Intensity"].Value.AttemptedValue;
            ViewBag.Exclude = ModelState["Exclude"].Value.AttemptedValue;
            ViewBag.Notes = ModelState["Notes"].Value.AttemptedValue;

            return View();
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return View();
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return View();
        }
    }
}