import { SetStateAction, useEffect, useState } from "react";

import { useForm } from "react-hook-form";
import { yupResolver } from "@hookform/resolvers/yup";
import * as yup from "yup";
// import { DateTimePicker } from "@/utils/DateTimePicker";
import { MdKeyboardBackspace } from "react-icons/md";
import DatePicker from "react-datepicker";
import "react-datepicker/dist/react-datepicker.css";
import { useNavigate } from "react-router-dom";
import { uploadFirebaseImage } from "../../../../apis/firebase";
import { notify } from "../../../../store/reducers/notify_reducers";
import { useDispatch } from "react-redux";
import { DepartmentType } from "../../department/Department";
import { PositionDTO } from "../../../position/Position";
import {
  GetDepartment,
  GetPositionByDepartmentCode,
  CreateNewEmployee,
} from "../../../../apis/api_function";

interface Employee {
  email: string;
  fullName: string;
  phoneNumber: string;
  gender: string;
  cmnd: string;
  address: string;
}

const schema = yup.object().shape({
  fullName: yup.string().required(),
  email: yup.string().email().required(),
  phoneNumber: yup.string().required(),
  gender: yup.string().required(),
  cmnd: yup.string().required(),
  address: yup.string().required(),
});

const CreateEmployee = () => {
  const navigate = useNavigate();
  const dispatch = useDispatch();
  const {
    register,
    handleSubmit,
    formState: { errors },
  } = useForm({
    mode: "onBlur",
    resolver: yupResolver(schema),
  });
  const [startDate, setStartDate] = useState(new Date());
  const [file, setFile] = useState<File | undefined>(undefined); // file state
  const [image, setImage] = useState<string | undefined>(undefined);
  const [loading, setLoading] = useState(false);
  const [department, setDepartment] = useState<DepartmentType[]>([]);
  const [position, setPosition] = useState<PositionDTO[]>([]);
  const [currentDepartment, setCurrentDepartment] = useState("");
  const [currentPosition, setCurrentPosition] = useState("");

  // useEffect(() => {
  //   if (currentPosition === "") {
  //     setCurrentPosition(position[0]?.id.toString());
  //   }
  // }, [position]);

  // useEffect(() => {
  //   if (currentDepartment === "") {
  //     setCurrentDepartment(department[0]?.department_code);
  //   }
  // }, [department]);

  function getCurrentDepartment(event: {
    target: { value: SetStateAction<string> };
  }) {
    setCurrentDepartment(event.target.value);
  }

  function getCurrentPosition(event: {
    target: { value: SetStateAction<string> };
  }) {
    setCurrentPosition(event.target.value);
  }

  async function GetPositionByDepartment(dep: string) {
    try {
      setLoading(true);
      const res = await GetPositionByDepartmentCode(dep, 1, 20);
      setPosition(res.data);
      console.log("res.data position", res.data);
      setLoading(false);
    } catch (error) {
      console.log(error);
    }
  }

  useEffect(() => {
    async function getDepartment() {
      try {
        const res = await GetDepartment(1, 100);
        setDepartment(res.data.list_dep);
      } catch (error) {
        console.log(error);
      }
    }
    getDepartment();
  }, [currentPosition]);

  useEffect(() => {
    if (department.length === 0 || !department) {
      // console.log("th1", department);
      return;
    } else if (currentDepartment === "" && department.length > 0) {
      // console.log("th2", department);
      setCurrentDepartment(department[0].department_code);
    } else if (currentDepartment !== "" && department.length > 0) {
      GetPositionByDepartment(currentDepartment);
    }
  }, [currentDepartment, currentPosition, department]);

  const handleFileChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    const file = event.target.files?.[0];
    setFile(file);
    if (file != null) {
      const reader = new FileReader();
      reader.onload = () => {
        const fileData = reader.result as string;
        setImage(fileData);
      };
      reader.readAsDataURL(file);
    }
  };

  async function submitImage() {
    // handle submitting the form
    console.log("file", file);
    if (file == null) {
      return;
    }
    try {
      const url = await uploadFirebaseImage(file);
      return url;
    } catch (error) {
      console.log(error);
      return "string";
    }
  }

  async function submit(data: Employee) {
    setLoading(true);
    // handle submitting the form
    if (file == null) {
      dispatch(
        notify({
          message: "Please choose image!",
          type: "error",
        })
      );
      setLoading(false);
      return;
    }
    if (currentPosition === "") {
      // setCurrentPosition(position[0]?.id.toString());
      setTimeout(() => {
        setCurrentPosition(position[0]?.id.toString());
      }, 500);
    }
    if (currentDepartment === "") {
      setCurrentDepartment(department[0]?.department_code);
    }

    const imgAvatar = await submitImage();

    const employee = {
      ...data,
      gender: data.gender === "Male" ? true : false,
      birth_day: startDate,
      avatar: imgAvatar,
    };

    try {
      let response;
      if (currentPosition === "") {
        response = await CreateNewEmployee(position[0].id.toString(), employee);
      } else {
        response = await CreateNewEmployee(currentPosition, employee);
      }
      if (response) {
        dispatch(
          notify({
            message: "Add employee success!",
            type: "success",
          })
        );
        setLoading(false);

        navigate(-1);
      }
    } catch (error: any) {
      if ((error.response.status = 400)) {
        dispatch(
          notify({
            message: "Email existed!",
            type: "error",
          })
        );
      } else
        dispatch(
          notify({
            message: "Add employee failed!",
            type: "error",
          })
        );
    }

    setLoading(false);
  }

  return (
    <div>
      <button
        onClick={() => {
          navigate("/employee/list");
        }}
        className="flex btn btn-link items-center gap-2"
      >
        <MdKeyboardBackspace />
        <p>Back</p>
      </button>
      <section className="mt-4">
        <h1 className="font-bold text-2xl text-gray-900 mb-4">
          Create Employee
        </h1>
        <div className="flex justify-between bg-white border rounded-md max-w-[80rem]">
          <form className="p-4" onSubmit={handleSubmit(submit)}>
            <div className="flex justify-between">
              <div className="grid grid-cols-2 gap-2">
                <div className="grid grid-cols-2 items-center">
                  <span className="font-bold">Department:</span>
                  <select
                    id="department"
                    className="select select-bordered w-full max-w-xs"
                    onChange={getCurrentDepartment}
                  >
                    {/* <option disabled selected>
                Department
              </option> */}
                    {department?.map((item: DepartmentType) => (
                      <option
                        key={item.department_code}
                        value={item.department_code}
                        className="w-full"
                      >
                        {item.name}
                      </option>
                    ))}
                  </select>
                </div>
                <div className="grid grid-cols-2 items-center">
                  <span className="font-bold">Position:</span>
                  <select
                    id="position"
                    className="select select-bordered w-full max-w-xs"
                    onChange={getCurrentPosition}
                  >
                    <option disabled>Position</option>
                    {position?.map((item: PositionDTO) => (
                      <option key={item.id} value={item.id}>
                        {item.title}
                      </option>
                    ))}
                  </select>
                </div>
                <div className="grid grid-cols-2 items-center">
                  <span className="font-bold">Full name:</span>
                  <input
                    type="text"
                    placeholder="Full name"
                    className="input input-bordered"
                    {...register("fullName")}
                  />
                  {errors.fullName && (
                    <p className="text-do-color text-sm mt-2">
                      Your name must be at least 6 characters as well.
                    </p>
                  )}
                </div>
                <div className="grid grid-cols-2 items-center">
                  <span className="font-bold">Email:</span>
                  <input
                    type="text"
                    placeholder="Email"
                    className="input input-bordered"
                    {...register("email")}
                  />
                  {errors.email && (
                    <p className="text-do-color text-sm mt-2">
                      Your email must valid.
                    </p>
                  )}
                </div>
                <div className="grid grid-cols-2 items-center">
                  <span className="font-bold">Phone:</span>
                  <input
                    type="text"
                    placeholder="Phone"
                    className="input input-bordered"
                    {...register("phoneNumber")}
                  />
                  {errors.phoneNumber && (
                    <p className="text-do-color text-sm mt-2">
                      Your phone must be at least 6 characters as well.
                    </p>
                  )}
                </div>
                <div className="grid grid-cols-2 items-center">
                  <span className="font-bold">Date of Birth:</span>
                  {/* <input
                      type="text"
                      placeholder="Date of Birth"
                      className="input input-bordered"
                      value={employee.dateOfBirth}
                    /> */}

                  {/* <div className="border border-gray-300 rounded-md p-2"> */}
                  <DatePicker
                    selected={startDate}
                    onChange={(date) => setStartDate(date as Date)}
                    className="input input-bordered max-w-[218px]"
                    // {...register("birth_day")}
                  />
                  {/* </div> */}
                </div>
                <div className="grid grid-cols-2 items-center">
                  <span className="font-bold">Address:</span>
                  <input
                    type="text"
                    placeholder="Address"
                    className="input input-bordered"
                    {...register("address")}
                  />
                  {errors.address && (
                    <p className="text-do-color text-sm mt-2">
                      Your address must be at least 6 characters as well.
                    </p>
                  )}
                </div>
                <div className="grid grid-cols-2 items-center">
                  <span className="font-bold">Id number:</span>
                  <input
                    type="text"
                    placeholder="Id number"
                    className="input input-bordered"
                    {...register("cmnd")}
                  />
                  {errors.cmnd && (
                    <p className="text-do-color text-sm mt-2">
                      Your id must be at least 6 characters as well.
                    </p>
                  )}
                </div>
              </div>
              {/* avatar */}
              <div className="min-w-[10rem]">
                <div className="flex flex-col gap-2 p-4 mx-4 ">
                  <div className="flex items-center justify-center">
                    <img
                      src={image}
                      alt="man"
                      className="h-[6rem] w-[6rem] rounded-full border border-gray-500 p-2"
                    />
                  </div>
                  <input
                    type="file"
                    className="file-input w-full max-w-xs"
                    onChange={handleFileChange}
                    multiple
                    accept="image/*"
                  />
                </div>
                <div className="grid grid-cols-2 items-center mx-2">
                  <span className="font-bold">Gender:</span>
                  <select
                    className="input input-bordered"
                    {...register("gender")}
                  >
                    <option value="Female">Female</option>
                    <option value="Male">Male</option>
                  </select>
                </div>
              </div>
            </div>
            <div className="flex justify-center items-center mt-4">
              <button
                className="btn bg-tim-color text-white hover:text-black"
                type="submit"
                disabled={loading}
              >
                {loading ? (
                  <span className="loading loading-lg"></span>
                ) : (
                  <span>Submit</span>
                )}
              </button>
            </div>
          </form>
        </div>
      </section>
    </div>
  );
};

export default CreateEmployee;
