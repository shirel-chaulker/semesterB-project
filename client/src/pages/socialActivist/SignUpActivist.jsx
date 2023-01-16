import React, { useState } from "react";
import { AddActivistToDB } from "../../services/userServices";

export const ProfileActivist = () => {
  const [addActivist, SetAddActivist] = useState({
    FirstName: "",
    LastName: "",
    Address: "",
    Email: "",
    PhoneNumber: "",
    TwitterAcount: "",
  });

  const handleAddActivist = async () => {
    let json = addActivist;
    await AddActivistToDB(json);
    SetAddActivist({});
    //to clear input
    document.querySelectorAll("input").forEach((input) => (input.value = ""));
  };

  return (
    <>
      <h3>Before we strted, please sign up</h3>
      <div className="student-inputs ">
        <div className="input-group mb-3">
          <span className="input-group-text">First name</span>
          <input
            className="form-control"
            type="text"
            onChange={(o) => {
              SetAddActivist({ ...addActivist, FirstName: o.target.value });
            }}
          />
        </div>
        <div className="input-group mb-3">
          <span className="input-group-text">Last Name</span>
          <input
            className="form-control"
            type="text"
            onChange={(o) => {
              SetAddActivist({ ...addActivist, LastName: o.target.value });
            }}
          />
        </div>
        <div className="input-group mb-3">
          <span className="input-group-text">Address</span>
          <input
            className="form-control"
            type="text"
            onChange={(o) => {
              SetAddActivist({ ...addActivist, Address: o.target.value });
            }}
          />
        </div>
        <div className="input-group mb-3">
          <span className="input-group-text">Email</span>
          <input
            className="form-control"
            type="text"
            onChange={(o) => {
              SetAddActivist({ ...addActivist, Email: o.target.value });
            }}
          />
        </div>
        <div className="input-group mb-3">
          <span className="input-group-text">PhoneNumber</span>
          <input
            className="form-control"
            type="text"
            onChange={(o) => {
              SetAddActivist({ ...addActivist, PhoneNumber: o.target.value });
            }}
          />
        </div>
        <div className="input-group mb-3">
          <span className="input-group-text">TwitterAcount</span>
          <input
            className="form-control"
            type="text"
            onChange={(o) => {
              SetAddActivist({ ...addActivist, TwitterAcount: o.target.value });
            }}
          />
        </div>
        <button className="btn btn-success" onClick={handleAddActivist}>
          Submit
        </button>
      </div>
    </>
  );
};
