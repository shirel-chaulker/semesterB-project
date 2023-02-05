import React, { useEffect, useState } from "react";
import { Link, useLocation, useNavigate } from "react-router-dom";
import { RingLoader } from "react-spinners";
import { getCampaignById } from "../../services/services";

import "./campaignStyle/campaignInfo.css";

export const CampaignInfo = () => {
  const [Campaign, SetCampaign] = useState();
  const navigate = useNavigate();
  const location = useLocation();
  const { CampaignID } = location.state;
  console.log(CampaignID, "im here");

  const initCampaignData = async () => {
    let campaign = await getCampaignById(CampaignID);
    //console.log(CampaignID, "im here");
    console.log(campaign, "hereee");
    SetCampaign(campaign);
  };

  useEffect(() => {
    initCampaignData();
  }, []);

  return Campaign && Campaign !== undefined ? (
    <div className="card campaign-container">
      <div className="card-title">
        {Campaign.CampaignID && <h5>Campaign ID: {Campaign.CampaignID}</h5>}
        {Campaign.CampaignName && (
          <h5>CampaignName: {Campaign.CampaignName}</h5>
        )}
        {Campaign.NonProfitName && (
          <h2 className="card-body">{Campaign.NonProfitName}</h2>
        )}
        {Campaign.Hashtag && <h5>Campaign hashtag: {Campaign.Hashtag}</h5>}
        {Campaign.Description && (
          <h5>Campaign description: {Campaign.Description}</h5>
        )}

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
