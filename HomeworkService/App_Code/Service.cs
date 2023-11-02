using System;
using System.Activities.Statements;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Xml.Linq;

// 참고: "리팩터링" 메뉴에서 "이름 바꾸기" 명령을 사용하여 코드, svc 및 config 파일에서 클래스 이름  "Service"를 변경할 수 있습니다.
public class Service : IService
{
    string connectionString = "Data Source=DESKTOP-BN4BV1L;Initial Catalog=eh;Persist Security Info=True;User ID=scott;Password=tiger";

    
    public bool Reg(string id, string pw, string name)
	{
		SqlConnection scn = new SqlConnection(connectionString);
        scn.Open();
        // string query = "INSERT INTO login(CID,CPW,CNAME) VALUES(id,pw,name)";
       
        SqlCommand scmd = new SqlCommand("Reg",scn);
        scmd.CommandType = System.Data.CommandType.StoredProcedure;
        SqlParameter idprm = new SqlParameter("@CID", id);
        scmd.Parameters.Add(idprm);
        SqlParameter pwprm = new SqlParameter("@CPW", pw);
        scmd.Parameters.Add(pwprm);
        SqlParameter nameprm = new SqlParameter("@CNAME", name);
        scmd.Parameters.Add(nameprm);
        SqlParameter mstateprm = new SqlParameter("@CSTATE", 1); //1이면 회원가입 2이면 로그인
       scmd.Parameters.Add(mstateprm);
       
       int result = 0;

        SqlParameter rparam = new SqlParameter("@CRESULT", result);

        scmd.Parameters.Add(rparam);

        rparam.Direction = System.Data.ParameterDirection.Output;
        scmd.ExecuteNonQuery();
        scn.Close();
        scn.Dispose();
        result = (int)rparam.Value;

        if (result == 1)

        {

            return true;

        }

        return false;
    }

    public string Login(string id, string pw)
    {
            SqlConnection scn = new SqlConnection(connectionString);
            scn.Open();
            // string query = "INSERT INTO login(CID,CPW,CNAME) VALUES(id,pw,name)";

            SqlCommand scmd = new SqlCommand("LOGINMEMBER", scn);
            scmd.CommandType = System.Data.CommandType.StoredProcedure;
            SqlParameter idprm = new SqlParameter("@CID", id);
            scmd.Parameters.Add(idprm);
            SqlParameter pwprm = new SqlParameter("@CPW", pw);
            scmd.Parameters.Add(pwprm);
            //OUPUT인거
            SqlParameter nameprm = new SqlParameter("@CNAME",SqlDbType.NVarChar);
            scmd.Parameters.Add(nameprm);
            nameprm.Direction = System.Data.ParameterDirection.Output;
            nameprm.Value = "";
            SqlParameter mstateprm = new SqlParameter("@CSTATE", 2); //1이면 회원가입 2이면 로그인
            scmd.Parameters.Add(mstateprm);

            int result = 0;

            SqlParameter rparam = new SqlParameter("@CRESULT", result);

            scmd.Parameters.Add(rparam);

            rparam.Direction = System.Data.ParameterDirection.Output;
            scmd.ExecuteNonQuery();
            scn.Close();
            scn.Dispose();
            result = (int)rparam.Value;

            if (result == 1)

            {
            
                return nameprm.Value.ToString();

            }
            return "";

    }

    public bool Logout(string id, string pw)
    {
        SqlConnection scn = new SqlConnection(connectionString);
        scn.Open();
        // string query = "INSERT INTO login(CID,CPW,CNAME) VALUES(id,pw,name)";

        SqlCommand scmd = new SqlCommand("LOGOUT", scn);
        scmd.CommandType = System.Data.CommandType.StoredProcedure;
        SqlParameter idprm = new SqlParameter("@CID", id);
        scmd.Parameters.Add(idprm);
        SqlParameter pwprm = new SqlParameter("@CPW", pw);
        scmd.Parameters.Add(pwprm);
        //OUPUT인거
        
        SqlParameter mstateprm = new SqlParameter("@CSTATE", 1); //1이면 회원가입 2이면 로그인
        scmd.Parameters.Add(mstateprm);

        int result = 0;

        SqlParameter rparam = new SqlParameter("@CRESULT", result);

        scmd.Parameters.Add(rparam);

        rparam.Direction = System.Data.ParameterDirection.Output;
        scmd.ExecuteNonQuery();
        scn.Close();
        scn.Dispose();
        result = (int)rparam.Value;

        if (result == 1)

        {

           return true;
        }

        return false;

    }

    public bool UnReg(string id, string pw)
    {
        SqlConnection scn = new SqlConnection(connectionString);
        scn.Open();
        // string query = "INSERT INTO login(CID,CPW,CNAME) VALUES(id,pw,name)";

        SqlCommand scmd = new SqlCommand("UNREG", scn);
        scmd.CommandType = System.Data.CommandType.StoredProcedure;
        SqlParameter idprm = new SqlParameter("@CID", id);
        scmd.Parameters.Add(idprm);
        SqlParameter pwprm = new SqlParameter("@CPW", pw);
        scmd.Parameters.Add(pwprm);
        //OUPUT인거
        

        int result = 0;

        SqlParameter rparam = new SqlParameter("@CRESULT", result);

        scmd.Parameters.Add(rparam);

        rparam.Direction = System.Data.ParameterDirection.Output;
        scmd.ExecuteNonQuery();
        scn.Close();
        scn.Dispose();
        result = (int)rparam.Value;

        if (result == 1)

        {

            return true;
        }

        return false;
    }

    public CompositeType GetDataUsingDataContract(CompositeType composite)
	{
		if (composite == null)
		{
			throw new ArgumentNullException("composite");
		}
		if (composite.BoolValue)
		{
			composite.StringValue += "Suffix";
		}
		return composite;
	}
}
