using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace Sisterly_v1
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        sisterlyDataContext sisterlyDataContext = new sisterlyDataContext();

        public bool acceptRequest(int projID,int  uID)
        {
            var req = (from u in sisterlyDataContext.CollabRequests
                       where u.UserID.Equals(uID) &&
                        u.ProjectID.Equals(projID)
                       select u).FirstOrDefault();

            if(req != null)
            {
                req.RequestStatus = "ACCEPTED";

                try
                {
                    sisterlyDataContext.SubmitChanges();
                    return true;
                }
                catch (Exception e)
                {
                    e.GetBaseException();
                    return false;
                }
            }

            return false;


        }

        public Post createPost(int userID, string title, string image, int likes)
        {
            var post = new Post
            {
                Likes = likes,
                Title = title,
                PostImage = image,  
                UserID = userID,
            };
            sisterlyDataContext.Posts.InsertOnSubmit(post);
            try
            {
                sisterlyDataContext.SubmitChanges();
                return post;
            }
            catch (Exception e)
            {
                e.GetBaseException();
                return null;
            }
        }

        public List<Project> GetFilteredProjects(string name)
        {
            var projects = new List<Project>();

            dynamic myprojects = (from p in sisterlyDataContext.Projects
                                  where p.Title.Contains(name)
                                  select p);

            foreach(Project p in myprojects)
            {
                projects.Add(p);
            }

            return projects;
        }

        public List<Post> GetPosts()
        {
            var posts = new List<Post>();   
            dynamic myposts = (from p in sisterlyDataContext.Posts
                               select p);

            foreach(Post p in myposts)
            {
                posts.Add(p);
            }

            return posts;
        }

        public Project GetProject(int id)
        {
            var project = (from p in sisterlyDataContext.Projects
                           where p.ID == id
                           select p).FirstOrDefault();  
            return project;         
        }

        public List<Project> GetProjects()
        {
            var posts = new List<Project>();
            dynamic myposts = (from p in sisterlyDataContext.Projects
                               select p);

            foreach (Project p in myposts)
            {
                posts.Add(p);
            }

            return posts;
        }

        public List<String> GetSkills(int id)
        {
            var posts = new List<String>();
            dynamic myposts = from users in sisterlyDataContext.RegUsers
                               join myskills in sisterlyDataContext.SkillUsers
                               on users.ID equals myskills.UserID
                               select new
                               {
                                   Skillname = myskills.Skill.SkillName,
                                 
                               };

            foreach (var skills in myposts)
            {
                posts.Add(skills.Skillname);
            }

            return posts;

        }

        public RegUser getUser(int id)
        {
            var user = (from u in sisterlyDataContext.RegUsers
                        where u.ID.Equals(id) 
                        select u).FirstOrDefault();
            return user;    
        }

        public RegUser login(string email, string password)
        {
           
             var webuser = (from u in sisterlyDataContext.RegUsers
                            where u.Email.Equals(email) &&
                            u.UserPassword.Equals(password)
                            select u).FirstOrDefault();

             return webuser;
       
        }

        public bool makeRequest(int userid, int projectid)
        {
            var requests = new CollabRequest
            {
                ProjectID = projectid,
                UserID = userid,
                RequestStatus = "0",
                DateMade = DateTime.Now,
            };
            sisterlyDataContext.CollabRequests.InsertOnSubmit(requests);
            try
            {
                sisterlyDataContext.SubmitChanges();
                return true;
            }
            catch (Exception e)
            {
                e.GetBaseException();
                return false;
            }
        }

        public bool Register(string email, string name, string password, string surname, string studyfield, char isMentor, string description)
        {
            var newUser = new RegUser
                {
                Email = email,
                UserName = name,
                UserPassword = password,
                Surname = surname,
                FieldOfStudy = studyfield,
                IsMentor = '1',
               // MentorDescription = description

                };
            sisterlyDataContext.RegUsers.InsertOnSubmit(newUser);
            try
            {
                sisterlyDataContext.SubmitChanges();
                return true;
            }
            catch (Exception e)
            {
                e.GetBaseException();
                return false;
            }
        }
        /* if the skill is registered succesfully, register a new skilluser, if that is fine as well return true else return false**/
        public bool regSkill(string name, int userid)
        {
            bool isReg = false;
            var skill = new Skill
            {
                SkillName = name,
            };
            sisterlyDataContext.Skills.InsertOnSubmit(skill);
            try
            {
                sisterlyDataContext.SubmitChanges();
                isReg= true;
            }
            catch (Exception e)
            {
                e.GetBaseException();
                isReg = false;
            }

            if (isReg)
            {
                var newsu = new SkillUser
                {
                    UserID = userid,
                    SkillID = skill.ID
                };
                sisterlyDataContext.SkillUsers.InsertOnSubmit(newsu);
                try

                {
                    sisterlyDataContext.SubmitChanges();
                    return true;
                }
                catch (Exception e)
                {
                    e.GetBaseException();
                    return false;
                }
            }

            return false;
           


        }
    }
}
