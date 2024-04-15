import { useForm } from "react-hook-form";
import { yupResolver } from "@hookform/resolvers/yup";
import * as yup from "yup";
// import { DateTimePicker } from "@/utils/DateTimePicker";
import { useState } from "react";
import DatePicker from "react-datepicker";

import { MdKeyboardBackspace } from "react-icons/md";

import "react-datepicker/dist/react-datepicker.css";
import { useNavigate } from "react-router-dom";

interface Employee {
  name: string;
  position: string;
  department: string;
  email: string;
  phone: string;
  address: string;
  age: string;
  gender: string;
  id: string;
}

const employee: Employee = {
  name: "John Doe",
  position: "Software Engineer",
  department: "IT",
  email: "johndoe@example.com",
  phone: "123-456-7890",
  address: "1234 Main St",
  age: "30",
  gender: "Male",
  id: "1234567890",
};

const schema = yup.object().shape({
  name: yup.string().required(),
  position: yup.string().required(),
  department: yup.string().required(),
  email: yup.string().email().required(),
  phone: yup.string().required(),
  address: yup.string().required(),
  age: yup.string().required(),
  gender: yup.string().required(),
  id: yup.string().required(),
});

const EmployeeProfile = () => {
  const navigate = useNavigate();
  const {
    register,
    handleSubmit,
    formState: { errors },
  } = useForm({
    mode: "onBlur",
    resolver: yupResolver(schema),
  });
  const [startDate, setStartDate] = useState(new Date());
  const [image, setImage] = useState("/man-avatar.png");

  const handleFileChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    const file = event.target.files?.[0];
    if (file) {
      const reader = new FileReader();
      reader.onload = () => {
        setImage(reader.result as string);
      };
      reader.readAsDataURL(file);
    }
  };

  function submit(data: Employee) {
    // handle submitting the form
    console.log(data);
    if (errors) {
      alert("Please check your input");
    }
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
          Employee Profile
        </h1>
        <div className="flex justify-between bg-white border rounded-md max-w-[80rem]">
          <form className="p-4" onSubmit={handleSubmit(submit)}>
            <div className="flex">
              <div className="grid grid-cols-2 gap-2">
                <div className="grid grid-cols-2 items-center">
                  <span className="font-bold">Name:</span>
                  <input
                    type="text"
                    placeholder="Name"
                    className="input input-bordered"
                    defaultValue={employee.name}
                    {...register("name")}
                  />
                  {errors.name && (
                    <p className="text-do-color text-sm mt-2">
                      Your password must be at least 6 characters as well.
                    </p>
                  )}
                </div>
                <div className="grid grid-cols-2 items-center">
                  <span className="font-bold">Position:</span>
                  <select
                    className="input input-bordered"
                    defaultValue={employee.position}
                    {...register("position")}
                  >
                    <option value="female">IT</option>
                    <option value="male">Manager</option>
                    <option value="Software Engineer">Software Engineer</option>
                  </select>
                </div>
                <div className="grid grid-cols-2 items-center">
                  <span className="font-bold">Department:</span>
                  <select
                    className="input input-bordered"
                    defaultValue={employee.department}
                    {...register("department")}
                  >
                    <option value="female">IT</option>
                    <option value="male">HR</option>
                    <option value="other">Other</option>
                  </select>
                </div>
                <div className="grid grid-cols-2 items-center">
                  <span className="font-bold">Email:</span>
                  <input
                    type="text"
                    placeholder="Email"
                    className="input input-bordered"
                    defaultValue={employee.email}
                    {...register("email")}
                  />
                </div>
                <div className="grid grid-cols-2 items-center">
                  <span className="font-bold">Phone:</span>
                  <input
                    type="text"
                    placeholder="Phone"
                    className="input input-bordered"
                    defaultValue={employee.phone}
                    {...register("phone")}
                  />
                </div>
                <div className="grid grid-cols-2 items-center">
                  <span className="font-bold">Date of Birth:</span>
                  {/* <input
                      type="text"
                      placeholder="Date of Birth"
                      className="input input-bordered"
                      value={employee.dateOfBirth}
                    /> */}

                  <div className="border border-gray-300 rounded-md p-2">
                    <DatePicker
                      selected={startDate}
                      onChange={(date) => setStartDate(date as Date)}
                    />
                  </div>
                </div>
                <div className="grid grid-cols-2 items-center">
                  <span className="font-bold">Address:</span>
                  <input
                    type="text"
                    placeholder="Address"
                    className="input input-bordered"
                    defaultValue={employee.address}
                    {...register("address")}
                  />
                </div>
                <div className="grid grid-cols-2 items-center">
                  <span className="font-bold">Age:</span>
                  <input
                    type="text"
                    placeholder="Age"
                    className="input input-bordered"
                    value={employee.age}
                  />
                </div>
              </div>
              {/* avatar */}
              <div className="min-w-[10rem]">
                <div className="flex flex-col gap-2 p-4 mx-4 border-l-2">
                  <div>
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
                  <h1 className="text-xl font-bold text-center">
                    {employee.name}
                  </h1>
                  <p className="text-center">ID: {employee.id}</p>
                  <div className="grid grid-cols-2 items-center">
                    <span className="font-bold">Gender:</span>
                    <select
                      className="input input-bordered"
                      defaultValue={employee.gender}
                      {...register("gender")}
                    >
                      <option value="female">Female</option>
                      <option value="male">Male</option>
                      <option value="other">Other</option>
                    </select>
                  </div>
                </div>
              </div>
            </div>
            <div className="flex justify-center items-center mt-4">
              <button className="btn bg-tim-color text-white hover:text-black ">
                Submit
              </button>
            </div>
          </form>
        </div>
      </section>
    </div>
  );
};

export default EmployeeProfile;
