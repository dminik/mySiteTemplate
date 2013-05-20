﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using Trackyt.Core.DAL.DataModel;
using Trackyt.Core.DAL.Extensions;
using Trackyt.Core.DAL.Repositories;
using Trackyt.Core.Services;
using Web.API.v11.Model;
using Web.Infrastructure.Error;
using Web.Infrastructure.Exceptions;

namespace Web.API.v11.Controllers
{
    [HandleJsonError]
    public class ApiV11Controller : Controller
    {
        private IApiService _apiService;
        private ITasksRepository _tasksRepository;
        private IDateTimeProviderService _dateTimeService;
        private IShareService _shareService;

        public ApiV11Controller(IApiService auth, ITasksRepository repository, IDateTimeProviderService date, IShareService shareService)
        {
            _apiService = auth;
            _tasksRepository = repository;
            _dateTimeService = date;
            _shareService = shareService;
        }

        [HttpPost]
        public JsonResult Authenticate(string email, string password)
        {
            CheckArgumentNotNullOrEmpty(email, "email");
            CheckArgumentNotNullOrEmpty(password, "password");

            var success = true;
            var apiToken = _apiService.GetApiToken(email, password);

            if (apiToken == null)
            {
                throw new UserNotAuthorizedException();
            }

            return Json(
                new
                {
                    success = success,
                    data = new { apiToken = apiToken }
                });
        }

        // Tasks

        // GET tasks/all
        [HttpGet]
        public JsonResult All(string apiToken)
        {
            CheckArgumentApiToken(apiToken);
            
            var userId = _apiService.GetUserByApiToken(apiToken).Id;
            var tasks = CreateTasksList(userId);

            return Json(
                new
                {
                    success = true,
                    data = new { tasks = tasks }
                },
                JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult Total(string apiToken)
        {
            CheckArgumentApiToken(apiToken);

            var userId = _apiService.GetUserByApiToken(apiToken).Id;
            var tasks = CreateTasksList(userId);

            return Json(
                new
                {
                    success = true,
                    data = new { total = tasks.Count }
                },
                JsonRequestBehavior.AllowGet);
        }

        // POST tasks/add
        [HttpPost]
        public JsonResult Add(string apiToken, string description)
        {
            CheckArgumentApiToken(apiToken);
            CheckArgumentNotNullOrEmpty(description, "description");

            var userId = _apiService.GetUserByApiToken(apiToken).Id;
            var task = new Task { Description = description, UserId = userId, Status = (int)TaskStatus.None };
            _tasksRepository.Save(task);

            return Json(
                new
                {
                    success = true,
                    data = new { task = CreateTaskDescriptor(task) }
                });
        }

        // DELETE tasks/delete
        [HttpDelete]
        public JsonResult Delete(string apiToken, int taskId)
        {
            CheckArgumentApiToken(apiToken);
            CheckArgumentLessThanZero(taskId, "taskId");

            var userId = _apiService.GetUserByApiToken(apiToken).Id;
            var task = _tasksRepository.Tasks.WithId(taskId);

            CheckTaskNotNull(taskId, task);
            _tasksRepository.Delete(task);

            return Json(
                new
                {
                    success = true,
                    data = new { id = taskId }
                });
        }

        [HttpDelete]
        public JsonResult DeleteAllDone(string apiToken)
        {
            CheckArgumentApiToken(apiToken);

            var userId = _apiService.GetUserByApiToken(apiToken).Id;
            var tasks = _tasksRepository.Tasks.WithUserIdAndDone(userId);

            foreach (var task in tasks)
            {
                _tasksRepository.Delete(task);
            }

            return Json(
                new
                {
                    success = true,
                    data = ""
                });
        }

        // PUT tasks/start

        [HttpPut]
        public JsonResult Start(string apiToken, int taskId)
        {
            CheckArgumentApiToken(apiToken);
            CheckArgumentLessThanZero(taskId, "taskId");

            var userId = _apiService.GetUserByApiToken(apiToken).Id;
            var task = _tasksRepository.Tasks.WithId(taskId);

            CheckTaskNotNull(taskId, task);
            StartAndSave(task);

            return Json(
                new
                {
                    success = true,
                    data = new { task = CreateTaskDescriptor(task) }
                });
        }

        // PUT tasks/stop

        [HttpPut]
        public JsonResult Stop(string apiToken, int taskId)
        {
            CheckArgumentApiToken(apiToken);
            CheckArgumentLessThanZero(taskId, "taskId");

            var userId = _apiService.GetUserByApiToken(apiToken).Id;
            var task = _tasksRepository.Tasks.WithId(taskId);

            CheckTaskNotNull(taskId, task);
            StopAndSave(task);

            return Json(
                new
                {
                    success = true,
                    data = new { task = CreateTaskDescriptor(task) }
                });
        }


        // PUT tasks/start/all
        // TODO: remove this API call
        [HttpPut]
        public JsonResult StartAll(string apiToken)
        {
            //CheckArgumentApiToken(apiToken);

            //var userId = GetUserIdByApiToken(apiToken);
            //var allTasks = _tasks.Tasks.WithUserId(userId);

            //foreach (var task in allTasks)
            //{
            //    StartAndSave(task);
            //}

            //return Json(
            //    new
            //    {
            //        success = true,
            //        data = (string)null
            //    });

            throw new NotSupportedException();
        }

        // PUT tasks/stop/all
        // TODO: remove this API call
        [HttpPut]
        public JsonResult StopAll(string apiToken)
        {
            //CheckArgumentApiToken(apiToken);

            //var userId = GetUserIdByApiToken(apiToken);
            //var allTasks = _tasks.Tasks.WithUserId(userId);

            //foreach (var task in allTasks)
            //{
            //    StopAndSave(task);
            //}

            //return Json(
            //    new
            //    {
            //        success = true,
            //        data = (string)null
            //    });

            throw new NotSupportedException();
        }

        [HttpPut]
        public ActionResult UpdatePosition(string apiToken, int taskId, int position)
        {
            CheckArgumentApiToken(apiToken);

            var userId = _apiService.GetUserByApiToken(apiToken).Id;
            var task = _tasksRepository.Tasks.WithId(taskId);

            CheckTaskNotNull(taskId, task);

            task.Position = position;
            _tasksRepository.Save(task);

            return Json(
                new
                {
                    success = true,
                    data = new { task = CreateTaskDescriptor(task) }
                });
        }

        [HttpPut]
        public ActionResult UpdateDescription(string apiToken, int taskId, string description)
        {
            CheckArgumentApiToken(apiToken);

            var userId = _apiService.GetUserByApiToken(apiToken).Id;
            var task = _tasksRepository.Tasks.WithId(taskId);

            CheckTaskNotNull(taskId, task);
            
            task.Description = description;
            _tasksRepository.Save(task);

            return Json(
                new
                {
                    success = true,
                    data = new { task = CreateTaskDescriptor(task) }
                });
        }

        [HttpPut]
        public ActionResult UpdatePlannedDate(string apiToken, int taskId, string plannedDate)
        {
            CheckArgumentApiToken(apiToken);

            var userId = _apiService.GetUserByApiToken(apiToken).Id;
            var task = _tasksRepository.Tasks.WithId(taskId);

            CheckTaskNotNull(taskId, task);

            if (string.IsNullOrEmpty(plannedDate))
            {
                task.PlannedDate = null;
            }
            else
            {
                task.PlannedDate = DateTime.ParseExact(plannedDate, "dd-MM-yyyy", CultureInfo.InvariantCulture);
            }
            _tasksRepository.Save(task);

            return Json(
                new
                {
                    success = true,
                    data = new { task = CreateTaskDescriptor(task) }
                });
        }

        [HttpGet]
        public ActionResult Done(string apiToken)
        {
            CheckArgumentApiToken(apiToken);

            var userId = _apiService.GetUserByApiToken(apiToken).Id;
            var tasks = CreateDoneTasksList(userId);

            return Json(
                new
                {
                    success = true,
                    data = new { tasks = tasks }
                },
                JsonRequestBehavior.AllowGet);
        }

        [HttpPut]
        public ActionResult Done(string apiToken, int taskId)
        {
            CheckArgumentApiToken(apiToken);

            var task = _tasksRepository.Tasks.WithId(taskId);

            CheckTaskNotNull(taskId, task);

            task.Done = true;
            _tasksRepository.Save(task);

            return Json(
                new
                {
                    success = true,
                    data = new { task = CreateTaskDescriptor(task) }
                });
        }

        [HttpGet]
        public ActionResult TotalDone(string apiToken)
        {
            CheckArgumentApiToken(apiToken);

            var userId = _apiService.GetUserByApiToken(apiToken).Id;

            return Json(
                new
                {
                    success = true,
                    data = new { totalDone = _tasksRepository.Tasks.WithUserIdAndDone(userId).Count() }
                },
                JsonRequestBehavior.AllowGet
            );
        }

        [HttpGet]
        public ActionResult ShareLink(string apiToken)
        {
            CheckArgumentApiToken(apiToken);

            var user = _apiService.GetUserByApiToken(apiToken);
            var shareLink = _shareService.CreateShareLink(user.Email);

            return Json(
                new
                {
                    success = true,
                    data = new { link = shareLink }
                },
                JsonRequestBehavior.AllowGet
                );
        }

        [HttpPut]
        public ActionResult Undo(string apiToken, int taskId)
        {
            CheckArgumentApiToken(apiToken);

            var task = _tasksRepository.Tasks.WithId(taskId);

            CheckTaskNotNull(taskId, task);

            task.Done = false;
            _tasksRepository.Save(task);

            return Json(
                new
                {
                    success = true,
                    data = new { task = CreateTaskDescriptor(task) }
                });
        }

        [HttpPut]
        public ActionResult UndoAll(string apiToken)
        {
            CheckArgumentApiToken(apiToken);

            var user = _apiService.GetUserByApiToken(apiToken);
            var tasks = _tasksRepository.Tasks.WithUserIdAndDone(user.Id);

            foreach (var task in tasks)
            {
                task.Done = false;
                _tasksRepository.Save(task);
            }

            return Json(
                new
                {
                    success = true,
                    data = new { tasks = tasks }
                }
                );

        }

        private IList<TaskDescriptor> CreateTasksList(int userId)
        {
            return _tasksRepository.Tasks.WithUserId(userId).Select(t => CreateTaskDescriptor(t)).ToList();
        }

        private IList<TaskDescriptor> CreateDoneTasksList(int userId)
        {
            return _tasksRepository.Tasks.WithUserIdAndDone(userId).Select(t => CreateTaskDescriptor(t)).ToList();
        }

        private TaskDescriptor CreateTaskDescriptor(Task t)
        {
            return new TaskDescriptor
            {
                id = t.Id,
                description = t.Description,
                createdDate = t.CreatedDate,
                startedDate = t.StartedDate,
                stoppedDate = t.StoppedDate,
                plannedDate = t.PlannedDate,
                status = t.Status,
                spent = GetTaskActualWork(t),
                position = t.Position,
                done = t.Done
            };
        }

        private int GetTaskActualWork(Task t)
        {
            var actualWork = t.ActualWork;

            if (t.Status == (int)TaskStatus.Started)
            {
                actualWork += GetDifferenceInSeconds(t.StartedDate, _dateTimeService.UtcNow);
            }

            return actualWork;
        }

        private int GetDifferenceInSeconds(DateTime? start, DateTime? stop)
        {
            if (start == null)
            {
                return 0;
            }

            if (stop == null)
            {
                return Convert.ToInt32(Math.Floor((_dateTimeService.UtcNow - start).Value.TotalSeconds));
            }

            return Convert.ToInt32(Math.Floor((stop - start).Value.TotalSeconds));
        }

        private void StartAndSave(Task task)
        {
            if (task.Status == (int)TaskStatus.None || task.Status == (int)TaskStatus.Stopped)
            {
                task.Status = (int)TaskStatus.Started;
                task.StartedDate = _dateTimeService.UtcNow;
                task.StoppedDate = null;
                _tasksRepository.Save(task);
            }
        }

        private void StopAndSave(Task task)
        {
            if (task.Status == (int)TaskStatus.Started)
            {
                task.Status = (int)TaskStatus.Stopped;
                task.StoppedDate = _dateTimeService.UtcNow;
                task.ActualWork += GetDifferenceInSeconds(task.StartedDate, task.StoppedDate);

                _tasksRepository.Save(task);
            }
        }

        private void CheckArgumentNotNullOrEmpty(string value, string name)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException(name);
            }
        }

        private void CheckArgumentApiToken(string apiToken)
        {
            if (string.IsNullOrEmpty(apiToken) || apiToken.Length != 32)
            {
                throw new ArgumentException("Provided Api token has wrong format.");
            }
        }

        private void CheckArgumentLessThanZero(int value, string name)
        {
            if (value < 0)
            {
                throw new ArgumentException("Provided value could not be less than zero.", name);
            }
        }

        // TODO: get rid of that check. Repository must throw such exception, in case of task does not exist
        private static void CheckTaskNotNull(int id, Task task)
        {
            if (task == null)
            {
                throw new Exception(string.Format("Task with id: {0} does not exists.", id));
            }
        }
    }
}
