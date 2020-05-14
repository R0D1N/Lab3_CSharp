using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
			return base.GetHashCode();
		}

		public virtual object DeepCopy()
		{
			return 0;
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
			return string.Format($"{diagnos} -  диагноз \n" +
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

	class Doctor : Person, IDateAndCopy, System.Collections.IEnumerable
	{
		private string specifacation;
		private Category category;
		private int workTime;
		private List<Diploma> diplomasList = new List <Diploma> ();
		private List<Patient> patientList = new List <Patient> ();



		private Person personalInformation;

		public Person PersonalInformation { get { return personalInformation; } set { personalInformation = value; } }

		public string Specifacation { get { return specifacation; } set { specifacation = value; } }

		public Category Category { get { return category; } set { category = value; } }

		public int WorkTime { get { return workTime; } set { workTime = value; } }

		public List<Diploma> DiplomasList { get { return diplomasList; } set { diplomasList = value; } }

		public List<Patient> PatientList { get { return patientList; } set { patientList = value; } }


		public Doctor()
		{
			personalInformation = new Person();
			specifacation = "default";
			category = Category.High;
			workTime = 99;
		}
		public Doctor(Person personalInformation, string specifacation, Category category, int workTime)
		{
			this.personalInformation = personalInformation;
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

			return string.Format($"{personalInformation} - данные врача \n" +
				$"{specifacation} - специальность \n" +
				$"{category} - категория врача \n" +
				$"{workTime} - стаж \n" +
				$"{diplomsInfo}\n" +
				$"{patientsInfo}\n");
		}

		public override string ToShortString()
		{
			return string.Format($"{personalInformation}\n" +
				$"{specifacation} - специальность \n" +
				$"{category} - категория врача \n" +
				$"{workTime} - стаж \n");
		}

		public object DeepCopy(Doctor doctor)
		{

			return doctor;
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

		public System.Collections.IEnumerator GetEnumerator()
		{
			return patientList.GetEnumerator();
		}

		public System.Collections.IEnumerator GetPatientWith(string diagnos)
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

			if (person_1.Equals(person_2))
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

			Console.ReadLine();
			// второе задание
			Doctor doctor = new Doctor(new Person("Валерий", "Жмишенко", new DateTime(1991, 6, 5)), "хирург", Category.High, 5000);
			doctor.AddDiplomas(new Diploma(), new Diploma("MDA", "The best", new DateTime(2002, 2, 4)));
			doctor.AddPatients(new Patient(person_1, "healthy", new DateTime(2020, 5, 14)), new Patient());
			Console.WriteLine("----------------------------------------\n\n2 задание: \n\n----------------------------------------\n" + doctor.ToShortString());
			Console.WriteLine();
			Console.ReadLine();
			//  третье задание
			Console.WriteLine("----------------------------------------\n\n3 задание: \n\n----------------------------------------\n" + doctor.PersonalInformation);
			Console.WriteLine();

			Console.ReadLine();
			// четвертое задание
			Console.WriteLine("----------------------------------------\n\n4 задание: \n\n----------------------------------------\n" + doctor.PersonalInformation);
			Doctor doctorCopy = (Doctor)doctor.DeepCopy(doctor);


			Console.WriteLine(doctor.ToShortString());
			doctor.Specifacation = "Анастезиолог";


			Console.WriteLine();
			Console.WriteLine(doctorCopy.ToShortString());


			Console.ReadLine();
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

			Console.ReadLine();
			// шестое задание
			Console.WriteLine("----------------------------------------\n\n6 задание: \n\n----------------------------------------\nЛюди поступившие сегодня:");
			foreach (Patient p in doctor.PatientList)
			{
				if (p.TimeOfEntering == DateTime.Today)
				{
					Console.WriteLine(p.Name + " " + p.Surname);
					Console.WriteLine(p);
				}
			}


			Console.ReadLine();
			// седьмое задание

			/*Console.WriteLine("7 задание: \n\nЛюди поступившие с диагнозом 'healthy':");
			doctor_1.GetPatientWith("healthy");*/

			Console.WriteLine("----------------------------------------\n\n7 задание: \n\n----------------------------------------\nЛюди поступившие с диагнозом 'healthy':");
			foreach (Patient p in doctor)
			{
				if (p.Diagnos == "healthy")
				{
					Console.WriteLine(p.Name + " " + p.Surname);
					Console.WriteLine(p);
				}
			}
			Console.ReadLine();
		}
	}

	public interface IDateAndCopy
	{
		object DeepCopy();
		DateTime Date { get; set; }
	}
	enum Category { High, First, Second }
}