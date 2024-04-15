const Employee = () => {
    return (
      <div>
        <div className="flex flex-col gap-4">
          <div className="flex justify-between mt-8">
            <h1 className="font-bold text-2xl text-gray-900">
              Employee List in{" "}
            </h1>
            <button className="btn bg-tim-color hover:text-black text-white">
              <p>Add Employee</p>
            </button>
          </div>
        </div>
      </div>
    );
  };
  
  export default Employee;
  