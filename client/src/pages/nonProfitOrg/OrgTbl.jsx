import React, { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { RingLoader } from "react-spinners";

import { GetNonProfitRep } from "../../services/userServices";

export const OrgTbl = () => {
  const [OrgArr, SetOrgArr] = useState();
  const navigate = useNavigate();

  let GetOrgData = async () => {
    let arrOrg = await GetNonProfitRep();
    let result = Object.values(arrOrg);
    SetOrgArr(result);
  };

  useEffect(() => {
    GetOrgData();
  });

  let GoToOrg = (OrgID) => {
    console.log(OrgID);

    navigate("/NonProfit", {
      state: {
        OrgID,
      },
    });
  };

  return (
    <div>
      <table class="table table-dark table-hover">
        <thead>
          <tr>
            <th scope="col">OrgID</th>
            <th scope="col">FullNameRep</th>
            <th scope="col">OrgName</th>
            <th scope="col">URL</th>
            <th scope="col">Email</th>
            <th scope="col">Description</th>
            <th scope="col">PhoneNumber</th>
          </tr>
        </thead>
        {OrgArr && OrgArr !== undefined ? (
          OrgArr.map((c) => {
            let {
              OrgID,
              FullNameRep,
              OrgName,
              URL,
              Email,
              Description,
              PhoneNumber,
            } = c;
            return (
              <tbody>
                <tr>
                  {/* <th scope="row"></th> */}
                  <td>
                    <button onClick={() => GoToOrg(OrgID)}>{OrgID}</button>
                  </td>
                  <td>{FullNameRep}</td>
                  <td>{OrgName}</td>
                  <td>{URL}</td>
                  <td>{Email}</td>
                  <td>{Description}</td>
                  <td>{PhoneNumber}</td>
                </tr>

                {/* <td>
                <Link
                  to="/donationPage"
                  onClick={() => {
                    SetProductArr(c);
                  }}
                >
                  <button className="btn btn-danger">
                    Click here to donate
                  </button>
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
    </div>
  );
};
