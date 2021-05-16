using CORE_HBKSOFTWARE.Interfaces;

namespace CORE_HBKSOFTWARE.Classes
{
    public class SlugCreator : ISlugCreator
    {
        public string SlugCreatorForAll(string name, string surname, string ID)
        {
            if (surname == "")
            {
                string slug = name + "-" + ID;
                return slug;
            }
            else
            {
                string slug = name + "-" + surname + "-" + ID;
                return slug;
            }
        }
    }
}
