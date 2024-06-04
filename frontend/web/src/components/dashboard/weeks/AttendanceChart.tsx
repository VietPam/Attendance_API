import { RootState } from "../../../store/store";
import {
  Chart as ChartJS,
  CategoryScale,
  LinearScale,
  BarElement,
  Title,
  Tooltip,
  Legend,
} from "chart.js";
import { Bar } from "react-chartjs-2";
import { useSelector } from "react-redux";

ChartJS.register(
  CategoryScale,
  LinearScale,
  BarElement,
  Title,
  Tooltip,
  Legend
);

export const options = {
  responsive: true,
  plugins: {
    legend: {
      position: "top" as const,
    },
    title: {
      display: true,
      // text: "Chart.js Bar Chart",
    },
  },
};

const labels = [
  "Monday",
  "Tuesday",
  "Wednesday",
  "Thursday",
  "Friday",
  "Saturday",
  "Sunday",
];

export default function AttendanceChart() {
  const attendances = useSelector(
    (state: RootState) => state.dashboard.data.attendance_ByWeek
  );
// console.log(attendances);
  const data = {
    labels,
    datasets: [
      {
        label: "On time",
        data: attendances.map((attendance) => attendance.attendance),
        backgroundColor: "rgb(0, 204, 153)",
      },
      {
        label: "Late",
        data: attendances.map((attendance) => attendance.late),
        backgroundColor: "rgba(53, 162, 235, 0.5)",
      },
      {
        label: "Absent",
        data: attendances.map((attendance) => attendance.absent),
        backgroundColor: "rgba(255, 99, 132, 0.5)",
      },
    ],
  };
  return <Bar options={options} data={data} />;
}
