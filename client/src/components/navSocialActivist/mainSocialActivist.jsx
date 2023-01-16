import React, { useState } from "react";
import { Route, Routes } from "react-router-dom";
import { ThemeContext } from "../../context/theme.context";
import { CampaignInfo } from "../../pages/campaigns/campaignInfo";
import { CampaignTbl } from "../../pages/campaigns/campaignTbl";
import { OneProduct } from "../../pages/products/oneProduct";
import { ProductTbl } from "../../pages/products/productTbl";
import { PurchasePage } from "../../pages/socialActivist/purchasePage";
import { ProfileActivist } from "../../pages/socialActivist/SignUpActivist";
import { NavSocialActivist } from "./navSocialActivist";

export const MainSocialActivist = (props) => {
  const [Campaign, setCampaign] = useState({});
  return (
    <div>
      <ThemeContext.Provider value={{ Campaign, setCampaign }}>
        <NavSocialActivist />
        <Routes>
          <Route path="/socialActivist" element={<ProfileActivist />} />
          <Route path="/campaignTbl" element={<CampaignTbl />} />
          <Route path="/productsTbl" element={<ProductTbl />} />
          <Route path="/campaignInfo" element={<CampaignInfo />} />
          <Route path="/productInfo" element={<OneProduct />} />
          <Route path="/purchasePage" element={<PurchasePage />} />
        </Routes>
      </ThemeContext.Provider>
    </div>
  );
};
