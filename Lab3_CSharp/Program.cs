using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;

namespace Class_training
{
	
	class Person : IDateAndCopy
	{

		// ------------------------------------Поля класса-----------------------------------------
		protected string name;
		protected string surname;
		protected System.DateTime bornDate;

		DateTime Date { get; set; }

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



		// реализация IdateAndCopy

		DateTime IDateAndCopy.Date { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

		// Конструкторы класса
		public Person(string name, string surname, System.DateTime bornDate)
		{
			this.name = name;
			this.surname = surname;
			this.bornDate = bornDate;
		}

		public Person()
		{
			name = "DefaultName";
			surname = "DefaultSurname";
			bornDate = DateTime.UtcNow;
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

		object IDateAndCopy.DeepCopy()
		{
			throw new NotImplementedException("Some error in IDateAndCopy class person");
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
			this.diagnos = diagnos;
			this.timeOfEntering = timeOfEntering;
		}

		public Patient()
		{
			diagnos = "You are healthy";
			timeOfEntering = DateTime.Now;
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

		public override string ToString()
		{
			return string.Format($"{personalInformation} - данные врача \n" +
				$"{specifacation} - специальность \n" +
				$"{category} - категория врача \n" +
				$"{workTime} - стаж \n" +
				$"{DiplomasList}\n" +
				$"{PatientList}\n");
		}

		public override string ToShortString()
		{
			return string.Format($"{personalInformation}\n" +
				$"{specifacation} - специальность \n" +
				$"{category} - категория врача \n" +
				$"{workTime} - стаж \n");
		}

		public override object DeepCopy()
		{
			return base.DeepCopy();
		}

		DateTime IDateAndCopy.Date { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

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
			


		}
	}

	public interface IDateAndCopy
	{
		object DeepCopy();
		DateTime Date { get; set; }
	}
	enum Category { High, First, Second }
}