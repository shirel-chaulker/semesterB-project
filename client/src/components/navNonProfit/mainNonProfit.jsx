import React, { useState } from "react";
import { Route, Routes } from "react-router-dom";
import { CampaignInfo } from "../../pages/campaigns/campaignInfo";
import { CampaignTbl } from "../../pages/campaigns/campaignTbl";
import { CreateCampaign } from "../../pages/campaigns/createCampaign";
//import { HomePageNonProfit } from "../../pages/campaigns/nonProfitOrg/homePage.non_profit";

import { OrgTbl } from "../../pages/nonProfitOrg/OrgTbl";
import { SignUpOrg } from "../../pages/nonProfitOrg/SignUpOrg";

import { UpdateCampaign } from "../../pages/campaigns/updateCampaign";
import { NavNonProfit } from "./navNonProfit";

export const MainNonProfit = () => {
  const [campaign, setCampaign] = useState({});
  return (
    <>
      <NavNonProfit />
      <Routes>
        <Route path="/signUpNonProfit" element={<SignUpOrg />} />
        <Route path="/orgTbl" element={<OrgTbl />} />
        <Route
          path="/campaignTbl"
          element={<CampaignTbl setCampaign={setCampaign} />}
        />
        <Route path="/campaignInfo" element={<CampaignInfo />} />
        <Route path="/createCamapign" element={<CreateCampaign />} />
        <Route path="/edit" element={<UpdateCampaign campaign={campaign} />} />
        <Route path="signUpNonProfit/OrgTbl" element={<OrgTbl />} />
      </Routes>
    </>
  );
};
