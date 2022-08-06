﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace Sisterly_v1
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        List<Project> GetProjects();

        [OperationContract]
        List<Project> GetFilteredProjects(string name);

        [OperationContract]
        Project GetProject(int id);

        [OperationContract]
        RegUser login(string email, string name);

        [OperationContract]
        bool Register(string email, string name, string password, string surname, string studyfield, char isMentor, string description);

        [OperationContract]
        bool regSkill(string name, int userID);

        [OperationContract]
        bool makeRequest(int userid, int projectid);

        [OperationContract]
        bool acceptRequest(int reqID, int user);

        [OperationContract]
        Post createPost(int userID, string title, string image, int likes);

        [OperationContract]
        List<Post> GetPosts();

        [OperationContract]
        RegUser getUser(int id);

        [OperationContract]
        List<String> GetSkills(int id);












    }
}
