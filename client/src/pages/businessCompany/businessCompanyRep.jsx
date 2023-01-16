import React, { useEffect, useState } from "react";
import { RingLoader } from "react-spinners";
import { Link, useNavigate } from "react-router-dom";
import { GetCompanies } from "../../services/userServices";
import { DeliveryReport } from "./deliveryReport";

export const BusinessCompanyRep = (props) => {
  const [CompaniesArr, SetcompaniesArr] = useState();
  const navigate = useNavigate();

  let GetCompaniesData = async () => {
    let ArrCompanies = await GetCompanies();
    console.log(ArrCompanies);
    let Arrcomp = Object.values(ArrCompanies);
    console.log(Arrcomp, "work");
    SetcompaniesArr(Arrcomp);
  };

  useEffect(() => {
    GetCompaniesData();
    console.log("check");
  }, []);

  let GoTOCompanies = (BusinessID) => {
    console.log(BusinessID);
    navigate("/oneCompany", {
      state: {
        BusinessID,
      },
    });
  };

  return (
    <div>
      <table class="table table-dark table-hover">
        <thead>
          <tr>
            <th scope="col">BusinessID</th>
            <th scope="col">CompanyName</th>
            <th scope="col">URL</th>
            <th scope="col">Email</th>
            <th scope="col">PhoneNumber</th>
            {/* <th scope="col"></th> */}
          </tr>
        </thead>
        {CompaniesArr && CompaniesArr !== undefined ? (
          CompaniesArr.map((c) => {
            let { BusinessID, CompanyName, URL, Email, PhoneNumber } = c;
            console.log(BusinessID, CompanyName, URL, Email, PhoneNumber);
            return (
              <tbody>
                <tr>
                  {/* <th scope="row"></th> */}
                  <td>
                    <button onClick={() => GoTOCompanies(BusinessID)}>
                      {BusinessID}
                    </button>
                  </td>
                  <td>{CompanyName}</td>
                  <td>{URL}</td>
                  <td>{Email}</td>
                  <td>{PhoneNumber}</td>
                </tr>
                {/* <td>
            <Link
              to="/edit"
              onClick={() => {
                setCampaign(c);
              }}
            >
              <button className="btn btn-danger">Edit</button>
            </Link>
          </td> */}
                <tr />
              </tbody>
            );
          })
        ) : (
          <tbody>
            <tr>
              <td colSpan={9}>
                <RingLoader className="spinner" color="#8d8de3" />
              </td>
            </tr>
          </tbody>
        )}
      </table>
      <Link to="/report">
        <button className="btn btn-primary">click for delivery Report</button>
      </Link>
    </div>
  );
};
