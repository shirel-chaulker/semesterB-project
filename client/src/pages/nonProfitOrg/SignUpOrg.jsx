import React, { useState } from "react";
import { Link } from "react-router-dom";

import { AddProfitRepToDB } from "../../services/userServices";
import { OrgTbl } from "./OrgTbl";

export const SignUpOrg = (props) => {
  const [addOrg, SetAddOrg] = useState({
    FullNameRep: "",
    OrgName: "",
    URL: "",
    Email: "",
    Description: "",
    PhoneNumber: "",
  });

  const handleAddNonProfit = async () => {
    let json = addOrg;
    await AddProfitRepToDB(json);
    SetAddOrg({});
    //to clear input
    document.querySelectorAll("input").forEach((input) => (input.value = ""));
  };

  return (
    <>
      <h3>If this is your first entry, please sign up</h3>
      <div className="student-inputs ">
        <div className="input-group mb-3">
          <span className="input-group-text"> Full Name Rep</span>
          <input
            className="form-control"
            type="text"
            onChange={(o) => {
              SetAddOrg({ ...addOrg, FullNameRep: o.target.value });
            }}
          />
        </div>
        <div className="input-group mb-3">
          <span className="input-group-text"> Org Name</span>
          <input
            className="form-control"
            type="text"
            onChange={(o) => {
              SetAddOrg({ ...addOrg, OrgName: o.target.value });
            }}
          />
        </div>
        <div className="input-group mb-3">
          <span className="input-group-text">URL</span>
          <input
            className="form-control"
            type="text"
            onChange={(o) => {
              SetAddOrg({ ...addOrg, URL: o.target.value });
            }}
          />
        </div>
        <div className="input-group mb-3">
          <span className="input-group-text">Email</span>
          <input
            className="form-control"
            type="text"
            onChange={(o) => {
              SetAddOrg({ ...addOrg, Email: o.target.value });
            }}
          />
        </div>
        <div className="input-group mb-3">
          <span className="input-group-text">Description</span>
          <input
            className="form-control"
            type="text"
            onChange={(o) => {
              SetAddOrg({ ...addOrg, Description: o.target.value });
            }}
          />
        </div>
        <div className="input-group mb-3">
          <span className="input-group-text"> Phone Number</span>
          <input
            className="form-control"
            type="text"
            onChange={(o) => {
              SetAddOrg({ ...addOrg, PhoneNumber: o.target.value });
            }}
          />
        </div>
        <button className="btn btn-success" onClick={handleAddNonProfit}>
          Submit
        </button>
      </div>
      <Link to="OrgTbl">
        <button className="btn btn-info" onClick={<OrgTbl />}>
          {" "}
          more organization{" "}
        </button>
      </Link>
    </>
  );
};
