import React, { useEffect, useState } from "react";
import { RingLoader } from "react-spinners";
import { GetActivists } from "../../services/userServices";

export const ActivistTbl = (props) => {
  const [ActivistArr, SetActivistArr] = useState();

  let GetActivistData = async () => {
    let activist = await GetActivists();
    let result = Object.values(activist);
    SetActivistArr(result);
  };

  useEffect(() => {
    GetActivistData();
  });
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
        {ActivistArr && ActivistArr !== undefined ? (
          ActivistArr.map((c) => {
            let {
              ActivistId,
              FirstName,
              LastName,
              Address,
              Email,
              PhoneNumber,
              TwitterAcount,
            } = c;
            return (
              <tbody>
                <tr>
                  {/* <th scope="row"></th> */}
                  <td>{ActivistId}</td>
                  <td>{FirstName}</td>
                  <td>{LastName}</td>
                  <td>{Address}</td>
                  <td>{Email}</td>
                  <td>{PhoneNumber}</td>
                  <td>{TwitterAcount}</td>
                </tr>
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
