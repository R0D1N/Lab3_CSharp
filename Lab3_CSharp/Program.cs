using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Collections;

namespace Class_training
{

	class Person : IDateAndCopy
	{

		// ------------------------------------Поля класса-----------------------------------------
		protected string name;
		protected string surname;
		protected System.DateTime bornDate;

		public DateTime Date { get; set; }

		public string Name
		{
			get { return name; }
			set { name = value; }
		}

		public string Surname
		{
			get { return surname; }
			set { surname = value; }
		}

		public System.DateTime BornDate
		{
			get { return bornDate; }
			set { bornDate = value; }
		}

		public int ChangeBornDate
		{
			get { return bornDate.Year; }
			set
			{
				DateTime bornYear = new DateTime(value, bornDate.Month, bornDate.Day);
				bornDate = bornYear;
			}
		}


		// Конструкторы класса
		public Person(string name, string surname, System.DateTime bornDate)
		{
			this.name = name;
			this.surname = surname;
			this.bornDate = bornDate;
		}

		public Person()
		{
			name = "Name";
			surname = "Surname";
			bornDate = new DateTime(2000, 1, 1);
		}

		// Методы
		public override string ToString()
		{
			return string.Format($"{name} -  имя \n" +
				$"{surname} -  фамилия \n" +
				$"{bornDate} - датa народження \n");
		}

		public virtual string ToShortString()
		{
			return name + " " + surname;
		}




		public static bool operator == (Person exam, Person exam2)
		{
			if (exam.Equals(exam2) == true)
				return true;
			else
				return false;
		}

		public static bool operator !=(Person exam, Person exam2)
		{
			if (exam.Equals(exam2) != true)
				return true;
			else
				return false;
		}

		public override bool Equals(object obj)
		{
			Person example = (Person)obj;
			if (Object.ReferenceEquals(example, null))
			{
				return false;
			}
			if (this.name == example.name && this.surname == example.surname && this.bornDate == example.bornDate)
			{
				return true;
			}
			else
				return false;
		}

		public override int GetHashCode()
		{
			return this.ToString().GetHashCode();
		}

		public virtual object DeepCopy()
		{
			return new Person(this.name, this.surname, this.bornDate);
		}

	}

	class Patient : Person
	{
		private string diagnos;
		private DateTime timeOfEntering;

		public string Diagnos
		{
			get { return diagnos; }
			set { diagnos = value; }
		}

		public DateTime TimeOfEntering
		{
			get { return timeOfEntering; }
			set { timeOfEntering = value; }
		}
		
		public Patient(Person information, string diagnos, DateTime timeOfEntering)
		{
			this.name = information.Name;
			this.surname = information.Surname;
			this.bornDate = information.BornDate;
			this.diagnos = diagnos;
			this.timeOfEntering = timeOfEntering;
		}

		public Patient()
		{
			diagnos = "healthy";
			timeOfEntering = new DateTime(2019, 12, 29);
		}
		public override string ToString()
		{
			return string.Format($"{Name} {Surname} \n" +
				$"{diagnos} -  диагноз \n" +
				$"{timeOfEntering} -  дата поступлеиния в стационар \n");
		}
	}

	class Diploma
	{

		public string orgName { get; set; }

		public string qualifications { get; set; }

		public DateTime diplomeDateTime { get; set; }



		public Diploma(string orgName, string qualifications, DateTime diplomeDateTime)
		{
			this.orgName = orgName;
			this.qualifications = qualifications;
			this.diplomeDateTime = diplomeDateTime;
		}

		public Diploma()
		{
			orgName = "DNU";
			qualifications = "default";
			diplomeDateTime = DateTime.UtcNow;
		}

		public override string ToString()
		{
			return string.Format($"{orgName} - назвaние организации, которая видала диплом (сертификат) \n" +
				$"{qualifications} -  полученая квалификация\n" +
				$"{diplomeDateTime} - датa получения диплома \n ");
		}
	}

	class Doctor : Person
	{
		private string specifacation;
		private Category category;
		private int workTime;
		private List<Diploma> diplomasList = new List <Diploma> ();
		private List<Patient> patientList = new List <Patient> ();




		public Person PersonalInformation { get { return new Person(this.Name, this.Surname, this.BornDate); } set { this.name = value.Name; this.surname = value.Surname; this.BornDate = value.BornDate; } }

		public string Specifacation { get { return specifacation; } set { specifacation = value; } }

		public Category Category { get { return category; } set { category = value; } }

		public int WorkTime { get { return workTime; } set { workTime = value; } }

		public List<Diploma> DiplomasList { get { return diplomasList; } set { diplomasList = value; } }

		public List<Patient> PatientList { get { return patientList; } set { patientList = value; } }


		public Doctor()
		{
			specifacation = "default";
			category = Category.High;
			workTime = 99;
		}
		public Doctor(Person personalInformation, string specifacation, Category category, int workTime)
			:base (personalInformation.Name, personalInformation.Surname, personalInformation.BornDate)
		{
			this.specifacation = specifacation;
			this.category = category;
			this.workTime = workTime;
		}

		

		public Diploma getFirstDiploma
		{
			get
			{
				if (DiplomasList == null)
				{
					System.DateTime firstDiplom = diplomasList[0].diplomeDateTime;
					int iMin = 0;
					int i = 0;
					foreach (Diploma p in diplomasList)
					{
						if (firstDiplom > p.diplomeDateTime)
						{
							firstDiplom = p.diplomeDateTime;
							iMin = i;
						}
						i++;
					}
					return diplomasList[iMin];
				}
				else
					return null;

			}
		}

		public void AddDiplomas(params Diploma[] aboutDiploma)
		{
			diplomasList.AddRange(aboutDiploma);
		}

		public void AddPatients(params Patient[] aboutPatients)
		{
			patientList.AddRange(aboutPatients);
		}

		public override string ToString()
		{
			string diplomsInfo = "";
			foreach (Diploma p in diplomasList)
			{
				diplomsInfo += p.ToString();
			}

			string patientsInfo = "";
			foreach (Patient t in patientList)
			{
				patientsInfo += t.ToString();
			}

			return string.Format($"{PersonalInformation}\n" +
				$"{specifacation}, " +
				$"{category}, " +
				$"{workTime} \n" +
				$"{diplomsInfo}\n" +
				$"{patientsInfo}\n");
		}

		public override string ToShortString()
		{
			return string.Format($"{PersonalInformation}\n" +
				$"{specifacation} - специальность \n" +
				$"{category} - категория врача \n" +
				$"{workTime} - стаж \n");
		}

		public object DeepCopy(Doctor doctor)
		{
			Doctor Copy = new Doctor()
			{
				PersonalInformation = this.PersonalInformation,
				Specifacation = this.Specifacation,
				Category = this.Category,
				workExperience = this.workExperience,
				PatientList = new List<Patient>(),
				DiplomasList = new List<Diploma>()
			};

			foreach (Patient p in PatientList)
			{
				Copy.PatientList.Add(new Patient(new Person(p.Name, p.Surname, p.BornDate), p.Diagnos, p.TimeOfEntering));
			}

			/*for (int i = 0; i < this.PatientList.Count; i++)
			{
				Copy.PatientList.Add(new Patient(new Person(this.PatientList[i].Name, this.PatientList[i].Surname, this.PatientList[i].BornDate), this.PatientList[i].Diagnos, this.PatientList[i].TimeOfEntering));
			}*/
			foreach (Diploma p in DiplomasList)
			{
				Copy.DiplomasList.Add(new Diploma(p.orgName, p.qualifications, p.diplomeDateTime));
			}

			/*for (int i = 0; i < this.PatientList.Count; i++)
			{
				Copy.DiplomasList.Add(new Diploma(this.DiplomasList[i].orgName, this.DiplomasList[i].qualifications, this.DiplomasList[i].diplomeDateTime));
			}
*/
			return Copy;
		}

		

		public int workExperience
		{
			get
			{
				return workTime;
			}
			set
			{
				if (workTime  < 0 || workTime > 100)
				{
					throw new Exception("Incorrect work time");
				}
				else
				{
					workTime = value;
				}
			}
		}


		public IEnumerator GetEnumerator()
		{
			return patientList.GetEnumerator();
		}

		public IEnumerable GetTodayPatient()
		{
			foreach (Patient p in patientList)
			{
				if (p.TimeOfEntering == DateTime.Today)
				{
					yield return p;
				}
			}
		}

		public IEnumerable GetPatientWith(string diagnos)
		{
			foreach (Patient p in patientList)
			{
				if (p.Diagnos == diagnos)
				{
					yield return p;
				}
			}
		}

	}



	class Program
	{
		static void Main()
		{
			// первое задание
			Person person_1 = new Person("Кирилл", "Родин", new DateTime(2002, 8, 17));
			Person person_2 = new Person("Кирилл", "Родин", new DateTime(2002, 8, 17));

			if (person_1 == person_2)
			{
				Console.WriteLine("----------------------------------------\n\n1 задание: \n\n----------------------------------------\n");
				Console.WriteLine("Они равны");
			}
			else
			{
				Console.WriteLine("----------------------------------------\n\n1 задание: \n\n----------------------------------------\n Они не равны");
			}

			Console.WriteLine($"hash code of person 1: {person_1.GetHashCode()}");
			Console.WriteLine();
			Console.WriteLine($"hash code of person 2: {person_2.GetHashCode()}");
			Console.WriteLine();

			// второе задание
			Doctor doctor = new Doctor(new Person("Валерий", "Жмишенко", new DateTime(1991, 6, 5)), "хирург", Category.High, 5000);
			doctor.AddDiplomas(new Diploma(), new Diploma("MDA", "The best", new DateTime(2002, 2, 4)));
			doctor.AddPatients(new Patient(person_1, "healthy", new DateTime(2020, 5, 15)), new Patient());
			Console.WriteLine("----------------------------------------\n\n2 задание: \n\n----------------------------------------\n" + doctor);
			Console.WriteLine();

			//  третье задание
			Console.WriteLine("----------------------------------------\n\n3 задание: \n\n----------------------------------------\n" + doctor.PersonalInformation);
			Console.WriteLine();

			// четвертое задание
			Console.WriteLine("----------------------------------------\n\n4 задание: \n\n----------------------------------------\n");
			Doctor doctorCopy = (Doctor)doctor.DeepCopy(doctor);
			doctor.PatientList[0].Name = "!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!";
			doctor.Specifacation = "&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&";
			Console.WriteLine(doctor);

			Console.WriteLine();
			Console.WriteLine(doctorCopy);

			// пятое задание
			try
			{
				doctor.workExperience = -10;
			}
			catch (Exception ex)
			{
				Console.WriteLine("----------------------------------------\n\n5 задание: \n\n----------------------------------------\n" + ex.Message);
			}
			Console.WriteLine();

			// шестое задание
			Console.WriteLine("----------------------------------------\n\n6 задание: \n\n----------------------------------------\nЛюди поступившие сегодня:");
			foreach (Patient p in doctor.GetTodayPatient())
			{
				Console.WriteLine(p);
			}
			// седьмое задание
			Console.WriteLine("----------------------------------------\n\n7 задание: \n\n----------------------------------------\nЛюди поступившие с диагнозом 'healthy':");

			
			foreach (Patient p in doctor.GetPatientWith("healthy"))
			{
				Console.WriteLine(p);
			}
		}
	}

	public interface IDateAndCopy
	{
		object DeepCopy();
		DateTime Date { get; set; }
	}
	enum Category { High, First, Second }
}