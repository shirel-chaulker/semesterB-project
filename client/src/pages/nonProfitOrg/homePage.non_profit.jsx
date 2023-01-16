import React, { useEffect, useState } from "react";
import { Link, useLocation, useNavigate } from "react-router-dom";
import { RingLoader } from "react-spinners";
import { getOrgById } from "../../services/userServices";
import "./homePageNonProfit.css";

export const HomePageNonProfit = () => {
  const [Org, SetOrg] = useState();
  const navigate = useNavigate();
  const location = useLocation();
  const { OrgID } = location.state;

  let initOrgData = async () => {
    let org = await getOrgById(OrgID);
    SetOrg(org);
  };

  useEffect(() => {
    initOrgData();
  }, []);

  return Org && Org !== undefined ? (
    <div className="card campaign-container">
      <div className="card-title">
        {Org.OrgID && <h5>OrgID: {Org.OrgID}</h5>}
        {Org.FullNameRep && <h5>FullNameRep: {Org.FullNameRep}</h5>}
        {Org.OrgName && <h2 className="card-body">{Org.OrgName}</h2>}
        {Org.URL && <h5>URL: {Org.URL}</h5>}
        {Org.Email && <h5>Email: {Org.Email}</h5>}
        {Org.Description && <h5>Description: {Org.Description}</h5>}
        {Org.PhoneNumber && <h5>PhoneNumber: {Org.PhoneNumber}</h5>}

        <Link to="/campaignTbl">
          <button className="btn btn-primary">Return To Campaigns table</button>
        </Link>
      </div>
    </div>
  ) : (
    <div className="spinner-app">
      <RingLoader color="#8d8de3" size={300} />
    </div>
  );
};
