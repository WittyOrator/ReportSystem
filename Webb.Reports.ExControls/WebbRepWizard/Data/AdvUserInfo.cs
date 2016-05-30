using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.IO;

namespace Webb.Reports.ReportWizard.WizardInfo
{
    public class UserGroup
    {
        string groupName;
        string groupDir;
        ArrayList _pUsers = new ArrayList();

        public string GroupName
        {
            get
            {
                return groupName;
            }
            set
            {
                groupName = value;
            }
        }

        public string GroupDir
        {
            get
            {
                return groupDir;
            }
            set
            {
                groupDir = value;
            }
        }
        public bool AssignUser(User user)
        {
            return AssignUser(user, false);
        }
        public bool AssignUser(User user, bool bRecursive)
        {
            // See if the user is already in the group:
            for (int i = 0; i < _pUsers.Count; i++)
            {
                User pUser = (User)_pUsers[i];
                if (pUser.CUserName == user.CUserName)
                    return false;
            }

            // Add user to the group and increase counter:
            _pUsers.Add(user);

            // Have the user refer to this group, if it
            // was not called by the user himself:
            if (bRecursive == false)
                user.AssignGroup(this, true);
            return true;

        }
        User FindUser(string name)
        {
            User pUser = null;

            for (int i = 0; i < _pUsers.Count; i++)
            {
                pUser = (User)_pUsers[i];

                if (pUser.CUserName == name)

                    return pUser;
            }

            return null;
        }

    }

    public class User
    {
        #region Auto Constructor By Macro 2010-10-13 9:27:12
        public User()
        {
            _cUserName = string.Empty;
            _cLocalDir = string.Empty;
            _cRemoteDir = string.Empty;
            _cSport = string.Empty;
            _iAccessPriority = 0;
            _iResourcePriority = 0;
            _iUserType = 0;
            _pGroups = new ArrayList();
        }
        #endregion
        public User(string name, string local, string remote, string sport, int access, int resource, int type, UserGroup group)
        {
            _cUserName = name;
            _cLocalDir = local;
            _cRemoteDir = remote;
            if (sport.Length > 0)
                _cSport = sport;
            else
                _cSport = "Football";

            _iAccessPriority = access;
            _iResourcePriority = resource;

            _pGroups = new ArrayList();
            if (group != null)
                _pGroups.Add(group);

            _iUserType = type;
        }


        string _cUserName;

        // User's local directory:
        string _cLocalDir;

        // User's remote directory:
        string _cRemoteDir;

        // User sport
        string _cSport;

        // Access priority:
        int _iAccessPriority;

        // Resource priority:
        int _iResourcePriority;

        // Type of the user, currently editor or coach.
        int _iUserType;

        // List of groups this user belongs to:
        ArrayList _pGroups = new ArrayList();

        public bool AssignGroup(UserGroup group)
        {
            return AssignGroup(group, false);
        }
        public bool AssignGroup(UserGroup group, bool bRecursive)
        {
            // See if the user is already a memeber of the group: 
            for (int i = 0; i < _pGroups.Count; i++)
            {
                if (((UserGroup)(_pGroups[i])).GroupName == group.GroupName)
                    return false;
            }

            // Add group:
            _pGroups.Add(group);

            // Have the group refer to this user, if it was
            // not called by the group itself:
            if (bRecursive == false)
                group.AssignUser(this, true);

            return true;
        }

        public string CUserName
        {
            get
            {
                return _cUserName;
            }
            set
            {
                _cUserName = value;
            }
        }

        public string CLocalDir
        {
            get
            {
                return _cLocalDir;
            }
            set
            {
                _cLocalDir = value;
            }
        }

        public string CRemoteDir
        {
            get
            {
                return _cRemoteDir;
            }
            set
            {
                _cRemoteDir = value;
            }
        }

        public string CSport
        {
            get
            {
                return _cSport;
            }
            set
            {
                _cSport = value;
            }
        }

        public int IAccessPriority
        {
            get
            {
                return _iAccessPriority;
            }
            set
            {
                _iAccessPriority = value;
            }
        }

        public int IResourcePriority
        {
            get
            {
                return _iResourcePriority;
            }
            set
            {
                _iResourcePriority = value;
            }
        }

        public int IUserType
        {
            get
            {
                return _iUserType;
            }
            set
            {
                _iUserType = value;
            }
        }

        public System.Collections.ArrayList PGroups
        {
            get
            {
                return _pGroups;
            }
            set
            {
                _pGroups = value;
            }
        }
    };

    public class Location
    {
        #region Auto Constructor By Macro 2010-10-13 13:37:54
        public Location()
        {
            _cName = string.Empty;
            _cIPAddress = string.Empty;
        }

        public Location(string p_cIPAddress, string p_cName)
        {
            _cName = p_cName;
            _cIPAddress = p_cIPAddress;
        }
        #endregion

        #region Auto Constructor By Macro 2010-10-13 13:37:00

        #endregion


        protected string _cName;

        // IP address of the location, if available:
        protected string _cIPAddress;

        public string CName
        {
            get
            {
                return _cName;
            }
            set
            {
                _cName = value;
            }
        }

        public string CIPAddress
        {
            get
            {
                return _cIPAddress;
            }
            set
            {
                _cIPAddress = value;
            }
        }

    };

    public class UserInfo
    {
        List<UserGroup> _pAllGroups = new List<UserGroup>();  // Pointer to all usergroups:     

        // Pointer to all users:
        List<User> _pAllUsers = new List<User>();

        // Pointer to all resources:
        ArrayList _pAllLocations = new ArrayList();

        #region Read String
        public static short ReadShort(System.IO.FileStream fs)
        {
            byte[] byTmp = new byte[2];

            fs.Read(byTmp, 0, 2);

            return BitConverter.ToInt16(byTmp, 0);
        }
        public static int ReadInt32(FileStream fs)
        {
            System.Text.StringBuilder strInt = new System.Text.StringBuilder();

            while (true)
            {
                char c = (char)fs.ReadByte();

                if (c == ' ') break;

                strInt.Append(c);
            }

            int n = Int32.Parse(strInt.ToString());

            return n;
        }

        public static string ReadString(FileStream fs, bool bTranslateAsc, bool bAddPos)
        {
            int len = 0;

            len = ReadInt32(fs);


            byte[] bytes = new byte[len];

            fs.Read(bytes, 0, len);

            string str = System.Text.Encoding.Default.GetString(bytes);

            if (bAddPos) fs.Position++;

            return str;
        }

        #endregion        //End Modify

        #region   UseGroup
        public bool AddGroup(string Name, string Local)
        {

            for (int i = 0; i < _pAllGroups.Count; i++)
            {
                if (_pAllGroups[i].GroupName == Name)
                    return false;
            }

            UserGroup userGroup = new UserGroup();

            userGroup.GroupName = Name;

            userGroup.GroupDir = Local;

            _pAllGroups.Add(userGroup);

            return true;
        }

        public UserGroup FindGroup(string name)
        {
            UserGroup _pUserGroup = null;

            for (int i = 0; i < _pAllGroups.Count; i++)
            {
                _pUserGroup = _pAllGroups[i];

                if (_pUserGroup.GroupName == name)

                    return _pUserGroup;
            }

            return null;
        }
        #endregion

        #region User
        bool AddUser(string name, string local, string remote, string sport, int access, int resource, int type, string groupName)
        {
            UserGroup group = null;

            if (groupName != null)
                group = FindGroup(groupName);
            else
                // If group name is not specified, then assign the user into the "Invisible" group.
                group = _pAllGroups[0];

            if (group != null)
            {
                // See if the user already exists:
                foreach (User puser in _pAllUsers)
                {
                    if (puser.CUserName == name) return false;
                }
                // Create new user:
                User pUser = new User(name, local, remote, sport, access, resource, type, null);

                // Insert user into collection:
                _pAllUsers.Add(pUser);

                // Assign the new user into the group:
                return group.AssignUser(pUser);
            }
            else
                return false;
        }
        User FindUser(string name)
        {
            User pUser = null;

            for (int i = 0; i < _pAllUsers.Count; i++)
            {
                pUser = (User)_pAllUsers[i];

                if (pUser.CUserName == name)

                    return pUser;
            }

            return null;
        }

        #endregion

        #region AddLocation
        protected bool AddLocation(string name, string ipAddr)
        {
            Location pLoc = null;
            for (int idx = 0; idx < _pAllLocations.Count; idx++)
            {
                pLoc = (Location)_pAllLocations[idx];

                // Don't add same resource
                if (pLoc.CName == name)
                    return false;
            }

            // Add new resource
            _pAllLocations.Add(new Location(name, ipAddr));
            return true;
        }
        protected bool DeleteLocation(string name)
        {
            Location pLoc = null;

            for (int i = 0; i < _pAllLocations.Count; i++)
            {
                pLoc = (Location)_pAllLocations[i];

                if (pLoc.CName == name)
                {
                    _pAllLocations.RemoveAt(i);

                    return true;
                }
            }

            return false;
        }
        #endregion


        public bool ReadUserInfoFromDisk(string strFileName)
        {
            bool bSuccess = true, bHaveUserInfoVersion = false;

            if (!File.Exists(strFileName)) return false;

            string bufferSec = string.Empty;

            FileStream fs = File.Open(strFileName, FileMode.Open, FileAccess.Read, FileShare.Read);

            try
            {

                String buffer = ReadString(fs, true, true);

                if (buffer[0] == 'U' && buffer[1] == 'I' && buffer[2] == 'V')
                {
                    // Get the total number of groups:
                    buffer = ReadString(fs, true, true);

                    bHaveUserInfoVersion = true;
                }

                int iTotGroups = int.Parse(buffer);

                UserGroup pGroup = null;

                while (--iTotGroups >= 0)
                {
                    #region  Out iTotGroups Loop

                    // Read in group begining marker:
                    buffer = ReadString(fs, true, true);

                    // Read in group name, directory and add the group:
                    buffer = ReadString(fs, true, true);

                    if (bHaveUserInfoVersion)
                    {
                        bufferSec = ReadString(fs, true, true);

                        AddGroup(buffer, bufferSec);
                    }
                    else
                    {
                        AddGroup(buffer, "none");
                    }

                    pGroup = FindGroup(buffer);

                    buffer = ReadString(fs, true, true);

                    if (buffer == "<")
                    {
                        do
                        {
                            #region Out Do Loop
                            string name;
                            string local;
                            string remote;
                            string sport = string.Empty;
                            string groupName;
                            string groupDir;

                            int access = 0;
                            int resource = 0;

                            User pUser = null;

                            UserGroup pTempGroup = null;

                            // Read in user data:
                            string szVersion = ReadString(fs, true, true);

                            if (szVersion[0] != 'U' && szVersion[1] != 'V')
                                name = szVersion;
                            else
                                name = ReadString(fs, true, true);

                            local = ReadString(fs, true, true);

                            remote = ReadString(fs, true, true);

                            if (szVersion.CompareTo("UV003") >= 0)
                            {
                                sport = ReadString(fs, true, true);
                            }
                            buffer = ReadString(fs, true, true);

                            access = int.Parse(buffer);

                            buffer = ReadString(fs, true, true);

                            resource = int.Parse(buffer);

                            // Version 1.0 addition, user type.
                            int type = 0;

                            if (szVersion.CompareTo("UV001") >= 0)
                            {
                                buffer = ReadString(fs, true, true);

                                type = int.Parse(buffer);
                            }
                            groupName = ReadString(fs, true, true);

                            if (szVersion.CompareTo("UV002") >= 0)
                            {
                                groupDir = ReadString(fs, true, true);
                            }
                            else
                            {
                                groupDir = "none";
                            }

                            // Add the group:
                            AddGroup(groupName, groupDir);

                            pTempGroup = FindGroup(groupName);

                            // Add the user:
                            AddUser(name, local, remote, sport, access, resource, type, groupName);

                            pUser = FindUser(name);

                            // see if we have more groups the user belongs to:
                            groupName = ReadString(fs, true, true);
                            if (groupName == ">")
                                // No more groups to add. Read next character:
                                groupName = ReadString(fs, true, true);
                            else
                            {	// More groups to add:
                                #region
                                do
                                {
                                    if (szVersion.CompareTo("UV002") >= 0)
                                    {
                                        groupDir = ReadString(fs, true, true);
                                    }
                                    else
                                    {
                                        groupDir = "none";
                                    }
                                    // Add the group:
                                    AddGroup(groupName, groupDir);

                                    pTempGroup = FindGroup(groupName);
                                    // Assign the group to the user:
                                    pUser.AssignGroup(pTempGroup);
                                    // Read in next data:
                                    groupName = ReadString(fs, true, true);

                                } while (groupName != ">");
                                #endregion

                                // No more groups to add. Read next character:
                                groupName = ReadString(fs, true, true);
                            }
                            if (groupName != "<") break;// No more users, break out loop:

                            #endregion
                        } while (true);
                    }

                    #endregion  Out iTotGroups Loop
                }
                // Get total number of locations:
                buffer = ReadString(fs, true, true);

                #region Locations
                int iTotLocs = int.Parse(buffer);

                // Read in locations:
                while (--iTotLocs >= 0)
                {
                    string cLocName;

                    string cIPAddr;

                    // Read location name and IP addr.
                    cLocName = ReadString(fs, true, true);

                    cIPAddr = ReadString(fs, true, true);

                    // Add the location:
                    AddLocation(cLocName, cIPAddr);
                }
                #endregion

            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
                bSuccess = false;
            }

            finally
            {
                fs.Close();

            }

            return bSuccess;
        }

    }
}
