// import { useState } from "react";
// import { UpdateDepartment } from "@/apis/api_function";
// import { useDispatch } from "react-redux";
import { DepartmentType } from "./Department";
import { useNavigate } from "react-router-dom";
import { MdOutlineFindInPage } from "react-icons/md";
import DeleteDepartment from "./DeleteDepartment";
// import ChangeDepartment from "./ChangeDepartment";

// import ChangeDepartmentModal from "./ChangeDepartmentModal";
// import LoadingPage from "@/utils/Loading";

interface DepartmentRowProps {
  item: DepartmentType;
  itemIndex: number;
}

export const DepartmentRow = ({ item, itemIndex }: DepartmentRowProps) => {
  const navigate = useNavigate();
  // const dispatch = useDispatch();
  // const [departmentCode, setDepartmentCode] = useState(""); // eslint-disable-line @typescript-eslint/no-unused-vars
  // const [Loading, setLoading] = useState(false);
  // eslint-disable-next-line @typescript-eslint/no-unused-vars
  // const [change, setChange] = useState(false);

  // const [departmentName, setDepartmentName] = useState("");
  // const [departmentCode, setDepartmentCode] = useState(""); // eslint-disable-line @typescript-eslint/no-unused-vars

  // async function handleSubmitChange() {
  //   if (Loading) return;
  //   setLoading(true);
  //   try {
  //     const res = await UpdateDepartment(
  //       item.id,
  //       departmentName,
  //       departmentCode
  //     );
  //     dispatch({
  //       type: "NOTIFY",
  //       payload: {
  //         type: "success",
  //         message: res.data.message,
  //       },
  //     });
  //     console.log(res);
  //     setLoading(false);
  //     window.location.reload();
  //     document.getElementById("btn-close")?.click();
  //     // eslint-disable-next-line @typescript-eslint/no-explicit-any
  //   } catch (error: any) {
  //     console.log(error);
  //     dispatch({
  //       type: "NOTIFY",
  //       payload: {
  //         type: "error",
  //         message: error.response.data.message,
  //       },
  //     });
  //     setLoading(false);
  //     window.location.reload();
  //     document.getElementById("btn-close")?.click();
  //   }
  // }

  return (
    <>
      <tr>
        <td>{itemIndex + 1}</td>
        <td>{item.name}</td>
        <td>{item.department_code}</td>
        <td>{item.nameBoss}</td>
        <td>{item.numberEmployee}</td>
        <td>{item.numberPosition}</td>

        {/* <td>{item.numberEmployee}</td>
      <td>{item.lastUpdate.toString().split("T")[0]}</td> */}
        <th className="flex gap-1">
          <button
            className="btn btn-ghost btn-xs text-green-800 border border-green-800"
            onClick={() =>
              navigate(`/position/${item.name}/${item.department_code}`)
            }
          >
            <MdOutlineFindInPage className="h-5 w-6" />
          </button>

          <DeleteDepartment item={item} />
          {/* <ChangeDepartment item={item} /> */}
        </th>
      </tr>
    </>
  );
};
