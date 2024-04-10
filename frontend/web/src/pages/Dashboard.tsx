import { useDispatch } from "react-redux";
import { GetDashboard } from "../apis/api_function";
import { useEffect } from "react";
import { addDashboard } from "../store/reducers/dashboard_reducers";
import UnderChart from "../components/dashboard/UnderChart";

export interface AttendanceType {
    thu: number;
    ngay: number;
    attendance: number;
    late: number;
    absent: number;
  }
  
  export interface DepartmentCount {
    department_name: string;
    department_code: string;
    emp_count: number;
  }
  
  export interface Data {
    employees_Today: AttendanceType;
    total_Employee: {
      man: number;
      woman: number;
      total: number;
    };
    attendance_ByWeek: AttendanceType[];
    emp_perDepts: DepartmentCount[];
  }
  
  const Dashboard = () => {
    const dispatch = useDispatch();
    // const [data, setData] = useState<Data>();
  
    useEffect(() => {
      async function getDashboard() {
        const res = await GetDashboard();
        const dataRes: Data = res.data;
        // setData(dataRes);
        dispatch(addDashboard({ data: dataRes }));
      }
      getDashboard();
    }, []);
  
    return (
      <>
        <div className="flex flex-col gap-4">
          {/* {/* <GroupChart /> */}
          <UnderChart /> 
        </div>
      </>
    );
  };
  
  export default Dashboard;
  