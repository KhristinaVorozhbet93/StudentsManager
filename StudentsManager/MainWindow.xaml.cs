﻿using Microsoft.EntityFrameworkCore;
using StudentsManager.Entities;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Markup;

namespace StudentsManager
{
    public partial class MainWindow : Window
    {
        private AppDbContext _db = new AppDbContext();
        private Student _student = new Student();
        private Visit _visit = new Visit();
        private Group _group = new Group();
        private Subject _subject = new Subject();
        private ObservableCollection<Student> _students;
        private ObservableCollection<Visit> _visits;
        private ObservableCollection<Group> _groups;
        private ObservableCollection<Subject> _subjects;
        const int StudentsPerPage = 100;
        Random random = new Random();
        public MainWindow()
        {
            InitializeComponent();
        }
        ~MainWindow()
        {
            _db.Dispose();
        }
        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Language = XmlLanguage.GetLanguage("ru-Ru");
            await LoadDefoltData();

            //await _db.Students.AddRangeAsync(Enumerable.Range(1, 1000).Select(number => new Student()
            //{
            //    Group = _groups[0],
            //    Name = $"Student{number}",
            //    Email = new Email("sdfhs@mail.ru"),
            //    Passport = new Passport(random.Next(1000000000, 2000000000).ToString())
            //}));
            //await _db.SaveChangesAsync();

        }
        private async void Add_NewStudent_Button_Click(object sender, RoutedEventArgs e)
        {
            if (studentNameTextBox.Text is not null
                && studentBirthdayDatePicker.SelectedDate is not null
                && studentEmailTextBox.Text is not null
                && studentPassportTextBox.Text is not null
                && studentPassportTextBox.Text is not null)
            {
                for (int i = 0; i < _students.Count; i++)
                {
                    if (studentPassportTextBox.Text == _students[i].Passport.ToString())
                    {
                        MessageBox.Show("Студент с такими паспортными данными уже существует!");
                        return;
                    }
                }
                Student student = new Student();
                try
                {
                    student = new Student()
                    {
                        Name = studentNameTextBox.Text,
                        Birthday = studentBirthdayDatePicker.SelectedDate,
                        Email = new Email(studentEmailTextBox.Text),
                        Group = (Group)studentGroupComboBox.SelectedItem,
                        Passport = new Passport(studentPassportTextBox.Text)
                    };
                }
                catch (ArgumentException exp)
                {
                    MessageBox.Show(exp.Message);
                    return;
                }

                await _db.Students.AddAsync(student);
                await _db.SaveChangesAsync();
                _students.Add(student);

                studentNameTextBox.Clear();
                studentBirthdayDatePicker.Text = null;
                studentEmailTextBox.Clear();
                studentPassportTextBox.Clear();
                studentGroupComboBox.SelectedIndex = 0;
            }
            else
            {
                MessageBox.Show("Заполните все поля!");
            }
        }
        private void Update_Student_Button_Click(object sender, RoutedEventArgs e)
        {
            if (studentsDataGrid.SelectedItem is null)
            {
                MessageBox.Show("Для редактирования нужно выбрать студента!");
            }
            else
            {
                _student = (Student)studentsDataGrid.SelectedItem;
                studentNameTextBox.Text = _student.Name;
                studentBirthdayDatePicker.SelectedDate = _student.Birthday;
                studentEmailTextBox.Text = _student.Email.ToString();
                studentPassportTextBox.Text = _student.Passport.ToString();
                studentGroupComboBox.SelectedItem = _student.Group;
            }
        }
        private async void Delete_Student_Button_Click(object sender, RoutedEventArgs e)
        {
            if (studentsDataGrid.SelectedItem is null)
            {
                MessageBox.Show("Для удаления нужно выбрать студента!");
            }
            else
            {
                _student = (Student)studentsDataGrid.SelectedItem;
                _db.Remove(_student!);
                await _db.SaveChangesAsync();
                _students.Remove(_student);
            }
        }
        private async void Save_Student_Button_Click(object sender, RoutedEventArgs e)
        {
            if (studentNameTextBox.Text is not null
                && studentBirthdayDatePicker.SelectedDate is not null
                && studentEmailTextBox.Text is not null
                && studentGroupComboBox.SelectedItem is not null
                && studentPassportTextBox.Text is not null)
            {
                for (int i = 0; i < _students.Count; i++)
                {
                    if (studentPassportTextBox.Text == _students[i].Passport.ToString())
                    {
                        MessageBox.Show("Студент с такими паспортными данными уже существует!");
                        return;
                    }
                }

                _student.Name = studentNameTextBox.Text;
                _student.Birthday = studentBirthdayDatePicker.SelectedDate;
                _student.Email = new Email(studentEmailTextBox.Text);
                _student.Passport = new Passport(studentPassportTextBox.Text);
                _student.Group = (Group)studentGroupComboBox.SelectedItem;

                await _db.SaveChangesAsync();

                studentsDataGrid.Items.Refresh();
                visitsDataGrid.Items.Refresh();

                studentNameTextBox.Clear();
                studentBirthdayDatePicker.Text = null;
                studentEmailTextBox.Clear();
                studentPassportTextBox.Clear();
                studentGroupComboBox.SelectedIndex = 0;
            }
            else
            {
                MessageBox.Show("Заполните все поля!");
            }
        }
        public Student? CurrentStudent => studentsDataGrid.SelectedItem as Student;
        private async void Add_NewVisit_Button_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentStudent is null)
            {
                MessageBox.Show("Нужно выбрать студента!");
            }
            else if (visitDatePicker.SelectedDate is not null
                && subjectsComboBox.SelectedItem is not null)
            {
                for (int i = 0; i < _visits.Count; i++)
                {
                    if (visitDatePicker.SelectedDate == _visits[i].Date
                        && studentsDataGrid.SelectedItem == _visits[i].Student
                        && subjectsComboBox.SelectedItem == _visits[i].Subject)
                    {
                        MessageBox.Show("Посещение с такими данными уже существует!");
                        return;
                    }
                }
                var visit = new Visit()
                {
                    Date = (DateTime)visitDatePicker.SelectedDate,
                    Student = (Student)studentsDataGrid.SelectedItem,
                    Subject = (Subject)subjectsComboBox.SelectedItem
                };
                await _db.Visits.AddAsync(visit);
                await _db.SaveChangesAsync();
                studentsDataGrid.Items.Refresh();
                _visits.Add(visit);
                visitDatePicker.Text = null;
                subjectsComboBox.SelectedIndex = 0;
            }
            else
            {
                MessageBox.Show("Нужно заполнить все поля!");
            }
        }
        private async void Delete_Visit_Button_Click(object sender, RoutedEventArgs e)
        {
            if (visitsDataGrid.SelectedItem is null)
            {
                MessageBox.Show("Для удаления нужно выбрать посещение!");
            }
            else
            {
                _visit = (Visit)visitsDataGrid.SelectedItem;
                _db.Remove(_visit!);
                await _db.SaveChangesAsync();
                _visits.Remove(_visit);
            }
        }
        private void Update_Visit_Button_Click(object sender, RoutedEventArgs e)
        {
            if (visitsDataGrid.SelectedItem is null)
            {
                MessageBox.Show("Для редактирования нужно выбрать посещение!");
            }
            else
            {
                _visit = (Visit)visitsDataGrid.SelectedItem;
                visitDatePicker.SelectedDate = _visit.Date;
                subjectsComboBox.SelectedItem = _visit.Subject;
            }
        }
        private async void Save_Visit_Button_Click(object sender, RoutedEventArgs e)
        {
            if (visitDatePicker.SelectedDate is not null
                && subjectsComboBox.SelectedItem is not null)
            {
                for (int i = 0; i < _visits.Count; i++)
                {
                    if (visitDatePicker.SelectedDate == _visits[i].Date
                        && studentsDataGrid.SelectedItem == _visits[i].Student
                        && subjectsComboBox.SelectedItem == _visits[i].Subject)
                    {
                        MessageBox.Show("Посещение с такими данными уже существует!");
                        return;
                    }
                }
                _visit.Date = (DateTime)visitDatePicker.SelectedDate;
                _visit.Subject = (Subject)subjectsComboBox.SelectedItem;

                await _db.SaveChangesAsync();

                visitsDataGrid.Items.Refresh();
                studentsDataGrid.Items.Refresh();
                visitDatePicker.Text = null;
                subjectsComboBox.SelectedIndex = 0;
            }
        }
        private async void Add_NewGroup_Button_Click(object sender, RoutedEventArgs e)
        {
            if (groupNameTextBox.Text is not null
                && groupCreatedAtDatePicker.SelectedDate is not null)
            {
                for (int i = 0; i < _groups.Count; i++)
                {
                    if (groupNameTextBox.Text == _groups[i].Name
                        && groupCreatedAtDatePicker.SelectedDate == _groups[i].CreatedAt)
                    {
                        MessageBox.Show("Группа с такими данными уже существует!");
                        return;
                    }
                }
                var group = new Group()
                {
                    Name = groupNameTextBox.Text,
                    CreatedAt = (DateTime)groupCreatedAtDatePicker.SelectedDate
                };
                await _db.Groups.AddAsync(group);
                await _db.SaveChangesAsync();
                _groups.Add(group);

                groupNameTextBox.Clear();
                groupCreatedAtDatePicker.Text = null;
            }
            else
            {
                MessageBox.Show("Заполните все поля!");
            }
        }
        private async void Delete_Group_Button_Click(object sender, RoutedEventArgs e)
        {
            if (groupsDataGrid.SelectedItem is null)
            {
                MessageBox.Show("Для удаления нужно выбрать группу!");
            }
            else
            {
                _group = (Group)groupsDataGrid.SelectedItem;
                _db.Remove(_group!);
                await _db.SaveChangesAsync();
                _groups.Remove(_group);
            }
        }
        private void Update_Group_Button_Click(object sender, RoutedEventArgs e)
        {
            if (groupsDataGrid.SelectedItem is null)
            {
                MessageBox.Show("Для редактирования нужно выбрать группу!");
            }
            else
            {
                _group = (Group)groupsDataGrid.SelectedItem;
                groupNameTextBox.Text = _group.Name;
                groupCreatedAtDatePicker.SelectedDate = _group.CreatedAt;
            }
        }
        private async void Save_Group_Button_Click(object sender, RoutedEventArgs e)
        {
            if (groupNameTextBox.Text is not null
                && groupCreatedAtDatePicker.SelectedDate is not null)
            {
                for (int i = 0; i < _groups.Count; i++)
                {
                    if (groupNameTextBox.Text == _groups[i].Name
                        && groupCreatedAtDatePicker.SelectedDate == _groups[i].CreatedAt)
                    {
                        MessageBox.Show("Группа с такими данными уже существует!");
                        return;
                    }
                }
                _group.Name = groupNameTextBox.Text;
                _group.CreatedAt = (DateTime)groupCreatedAtDatePicker.SelectedDate;

                await _db.SaveChangesAsync();

                groupsDataGrid.Items.Refresh();
                groupNameTextBox.Clear();
                groupCreatedAtDatePicker.Text = null;
            }
        }
        private async void Add_NewSubject_Button_Click(object sender, RoutedEventArgs e)
        {
            if (subjectNameTextBox.Text is not null)
            {
                for (int i = 0; i < _subjects.Count; i++)
                {
                    if (subjectNameTextBox.Text == _subjects[i].Name)
                    {
                        MessageBox.Show("Данный предмет уже существует!");
                        return;
                    }
                }
                var subject = new Subject()
                {
                    Name = subjectNameTextBox.Text
                };
                await _db.Subjects.AddAsync(subject);
                await _db.SaveChangesAsync();
                _subjects.Add(subject);
                subjectNameTextBox.Clear();
            }
            else
            {
                MessageBox.Show("Заполните все поля!");
            }
        }
        private async void Delete_Subject_Button_Click(object sender, RoutedEventArgs e)
        {
            if (subjectsDataGrid.SelectedItem is null)
            {
                MessageBox.Show("Для удаления нужно выбрать предмет!");
            }
            else
            {
                _subject = (Subject)subjectsDataGrid.SelectedItem;
                _db.Remove(_subject!);
                await _db.SaveChangesAsync();
                _subjects.Remove(_subject);
            }
        }
        private void Update_Subject_Button_Click(object sender, RoutedEventArgs e)
        {
            if (subjectsDataGrid.SelectedItem is null)
            {
                MessageBox.Show("Для редактирования нужно выбрать предмет!");
            }
            else
            {
                _subject = (Subject)subjectsDataGrid.SelectedItem;
                subjectNameTextBox.Text = _subject.Name;
            }
        }
        private async void Save_Subject_Button_Click(object sender, RoutedEventArgs e)
        {
            if (subjectNameTextBox.Text is not null)
            {
                for (int i = 0; i < _subjects.Count; i++)
                {
                    if (subjectNameTextBox.Text == _subjects[i].Name)
                    {
                        MessageBox.Show("Данный предмет уже существует!");
                        return;
                    }
                }
                _subject.Name = subjectNameTextBox.Text;

                await _db.SaveChangesAsync();

                subjectsDataGrid.Items.Refresh();
                subjectNameTextBox.Clear();
            }
        }

        DebounceService debounce = new DebounceService();
        bool isDefaultData = false;

        private async void TextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            _studentsPageIndex = 0;
            studentsScrollViewer.ScrollToHome(); 
            if (string.IsNullOrWhiteSpace(searchTextBox.Text))
            {
                await LoadDefoltData();              
                return;
            }
            await debounce.Debounce(TimeSpan.FromSeconds(1), Search, searchTextBox.Text);
            isDefaultData = false;
        }
       
   
        private async Task LoadDefoltData()
        {
            if (isDefaultData) return;
            isDefaultData = true;
            await LoadStudents();

            var visits = await _db.Visits
                .Include(visit => visit.Student)
                .Include(visit => visit.Subject)
                .ToListAsync();
            _visits = new ObservableCollection<Visit>(visits);
            visitsDataGrid.ItemsSource = _visits;

            var groups = await _db.Groups.ToListAsync();
            _groups = new ObservableCollection<Group>(groups);
            groupsDataGrid.ItemsSource = _groups;

            var subjects = await _db.Subjects.ToListAsync();
            _subjects = new ObservableCollection<Subject>(subjects);
            subjectsDataGrid.ItemsSource = _subjects;

            studentGroupComboBox.ItemsSource = _groups;
            subjectsComboBox.ItemsSource = _subjects;
        }

        int _studentsPageIndex = 0;
        int _studentsCount = 0;
        private async Task LoadStudents()
        {       
            _studentsCount = await _db.Students.CountAsync();
            var students = await _db.Students
                .OrderBy(it => it.Name)
                    .ThenBy(it => it.Birthday)
                .Skip(_studentsPageIndex * StudentsPerPage)
                .Take(StudentsPerPage)
                .ToListAsync();
            _students = new ObservableCollection<Student>(students);
            studentsDataGrid.ItemsSource = _students;

            prevStudentsPageButton.IsEnabled = _studentsPageIndex > 0;

            var pageCount = Math.Ceiling((double)_studentsCount / StudentsPerPage);
            var lastPageIndex = pageCount - 1;
            nextStudentsPageButton.IsEnabled = _studentsPageIndex < lastPageIndex;
        }

        private async Task LoadStudents(string text)
        {                 
            var matchesStudents = _db.Students
                .AsQueryable()
                .OrderBy(s => s.Name)
                .Where(s => s.Name.Contains(text) || s.Group!.Name.Contains(text));
            var students = await matchesStudents
                .Skip(_studentsPageIndex * StudentsPerPage)
                .Take(StudentsPerPage)
                .ToListAsync();
            _students = new ObservableCollection<Student>(students);
            studentsDataGrid.ItemsSource = _students;

            _studentsCount = await matchesStudents.CountAsync();
            prevStudentsPageButton.IsEnabled = _studentsPageIndex > 0;

            var pageCount = Math.Ceiling((double)_studentsCount / StudentsPerPage);
            var lastPageIndex = pageCount - 1;
            nextStudentsPageButton.IsEnabled = _studentsPageIndex < lastPageIndex;
  
        }
        public async Task Search(string textSpanshot)
        {
            await LoadStudents(textSpanshot);

            var matchesVisits = await _db.Visits
                .Where(v => v.Student!.Name.Contains(textSpanshot) || v.Subject!.Name.Contains(textSpanshot))
                .ToListAsync();

            visitsDataGrid.ItemsSource = matchesVisits;

            var matchesGroups = await _db.Groups
                .Where(g => g.Name.Contains(textSpanshot) || g.Students!.Any(student => student.Name.Contains(textSpanshot)))
                .ToListAsync();

            groupsDataGrid.ItemsSource = matchesGroups;

            var matchesSubjects = await _db.Subjects
                .Where(s => s.Name.Contains(textSpanshot))
                .ToListAsync();

            subjectsDataGrid.ItemsSource = matchesSubjects;
      
        }

        private async void ButtonPrevStudentsPage_Click(object sender, RoutedEventArgs e)
        {
            _studentsPageIndex--;
            studentsScrollViewer.ScrollToHome();
            if (string.IsNullOrWhiteSpace(searchTextBox.Text))
            {
                await LoadStudents();
            }
            else
            {
                await LoadStudents(searchTextBox.Text);
            }
        }
        private async void ButtonNextStudentsPage_Click(object sender, RoutedEventArgs e)
        {
            _studentsPageIndex++;
            studentsScrollViewer.ScrollToHome();
            if (string.IsNullOrWhiteSpace(searchTextBox.Text))
            {
                await LoadStudents();
            }
            else
            {
                await LoadStudents(searchTextBox.Text);
            }
        }
    }
}
