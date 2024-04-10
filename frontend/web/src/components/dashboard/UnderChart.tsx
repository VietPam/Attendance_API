import AttendanceChart from "./weeks/AttendanceChart";

const UnderChart = () => {
    return (
      <>
        <div className="grid grid-cols-1 gap-4 pb-8">
          <div className="bg-white p-4 border rounded-xl">
            <h3 className="text-lg font-semibold py-4">Attendance by week</h3>
            <div className="flex justify-center">
              <div className="w-full h-full max-w-[60rem]">
                <AttendanceChart />
              </div>
            </div>
          </div>
          {/* <div className="bg-white p-4 border rounded-xl">
            <LeaveRequest />
          </div> */}
          {/* <PayrollAnalysis /> */}
        </div>
      </>
    );
  };
  
  export default UnderChart;
  