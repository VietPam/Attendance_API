import {
    // MdOutlineDeleteForever,
    MdOutlineEdit,
    MdOutlineFindInPage,
  } from "react-icons/md";
  import { Link } from "react-router-dom";
  import { PositionDTO } from "./Position";
  
  interface PositionRowProps {
    // eslint-disable-next-line @typescript-eslint/no-explicit-any
    item: PositionDTO;
    index: number;
  }
  
  export const PositionRow = ({ item, index }: PositionRowProps) => {
    return (
      <tr>
        <td>{index + 1}</td>
        <td>{item.code}</td>
        <td>{item.title}</td>
        <td>{item.emp_count}</td>
        <td>{item.salary_coeffcient}</td>
        <th className="flex gap-1">
          <Link to={`${item.id}/${item.title}`}>
            <button className="btn btn-ghost btn-xs border border-green-700 text-green-700">
              <MdOutlineFindInPage className="h-5 w-5" />
            </button>
          </Link>
          <Link
            to={`change/${item.id}/${item.title}/${item.code}/${item.salary_coeffcient}`}
          >
            <button className="btn btn-ghost btn-xs border border-gray-600">
              <MdOutlineEdit className="h-5 w-5" />
            </button>
          </Link>
          {/* <Link to={`delete/${item.id}`}>
            <button className="btn btn-ghost btn-xs text-red-600 border border-red-600">
              <MdOutlineDeleteForever className="h-5 w-5" />
            </button>
          </Link> */}
        </th>
      </tr>
    );
  };
  