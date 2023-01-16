import React, { useState } from "react";
import { AddPurchaseToDB } from "../../services/userServices";

export const PurchasePage = () => {
  const [Buy, SetBuy] = useState({
    ProductName: "",
    FullName: "",
    Address: "",
    PhoneNumber: "",
    CampaignDonation: "",
  });

  const HandleAddBuy = async () => {
    let json = Buy;
    await AddPurchaseToDB(json);
    SetBuy({});
    document.querySelectorAll("input").forEach((input) => (input.value = ""));
  };

  return (
    <>
      <h3>Fill in the details to buy the product</h3>
      <div className="student-inputs ">
        <div className="input-group mb-3">
          <span className="input-group-text">ProductName</span>
          <input
            className="form-control"
            type="text"
            onChange={(o) => {
              SetBuy({ ...Buy, ProductName: o.target.value });
            }}
          />
        </div>
        <div className="input-group mb-3">
          <span className="input-group-text"> FullName</span>
          <input
            className="form-control"
            type="text"
            onChange={(o) => {
              SetBuy({ ...Buy, FullName: o.target.value });
            }}
          />
        </div>
        <div className="input-group mb-3">
          <span className="input-group-text"> Address</span>
          <input
            className="form-control"
            type="text"
            onChange={(o) => {
              SetBuy({ ...Buy, Address: o.target.value });
            }}
          />
        </div>
        <div className="input-group mb-3">
          <span className="input-group-text">PhoneNumber</span>
          <input
            className="form-control"
            type="text"
            onChange={(o) => {
              SetBuy({ ...Buy, PhoneNumber: o.target.value });
            }}
          />
        </div>
        <div className="input-group mb-3">
          <span className="input-group-text">CampaignDonation</span>
          <input
            className="form-control"
            type="text"
            onChange={(o) => {
              SetBuy({ ...Buy, CampaignDonation: o.target.value });
            }}
          />
        </div>
        <button className="btn btn-success" onClick={HandleAddBuy}>
          Submit
        </button>
      </div>
    </>
  );
};
