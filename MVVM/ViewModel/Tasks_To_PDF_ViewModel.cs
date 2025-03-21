﻿using MiniProject.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MiniProject.MVVM.ViewModel
{
    public class TaskViewModel
    {
        private List<Services.Task> _tasks = new List<Services.Task>();

        // Property to expose the list of tasks (for data binding)
        public List<Services.Task> Tasks => _tasks;

        // Command to add a task
        public ICommand AddTaskCommand { get; }

        // Command to generate a PDF
        public ICommand GeneratePdfCommand { get; }

        public TaskViewModel()
        {
            // Initialize commands
            AddTaskCommand = new RelayCommand(AddTask);
            GeneratePdfCommand = new RelayCommand(GeneratePdf);
        }

        private void AddTask(object Written_Task)
        {
            if (TaskValidator.Validate((Services.Task)Written_Task))
            {
                _tasks.Add((Services.Task)Written_Task);
            }
        }

        private void GeneratePdf(object parameter)
        {
            // Specify the file path for the PDF
            string filePath = "TaskList.pdf";

            // Call the PdfGenerator to create the PDF
            Services.PDF_Gerator.GeneratePdf(_tasks, filePath);
        }

        private static class TaskValidator
        {
            public static bool Validate(Services.Task task)
            {
                if (string.IsNullOrEmpty(task.Title))
                    return false;

                if (task.FinishDate < DateTime.Now)
                    return false;

                return true;
            }
        }
    }
}
