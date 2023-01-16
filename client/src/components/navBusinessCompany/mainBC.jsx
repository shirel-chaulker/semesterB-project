import React, { useState } from "react";
import { Route, Routes } from "react-router-dom";
import { ThemeContext } from "../../context/theme.context";
import { BusinessCompanyRep } from "../../pages/businessCompany/businessCompanyRep";
import { DeliveryReport } from "../../pages/businessCompany/deliveryReport";
import { CampaignInfo } from "../../pages/campaigns/campaignInfo";
import { CampaignTbl } from "../../pages/campaigns/campaignTbl";
import { DonationsPage } from "../../pages/products/donations.Pages.jsx";
import { OneProduct } from "../../pages/products/oneProduct";
import { ProductTbl } from "../../pages/products/productTbl";
import { NavBC } from "./navBC";

export const MainBC = (props) => {
  const [Campaign, setCampaign] = useState({});
  return (
    <div>
      <ThemeContext.Provider value={{ Campaign, setCampaign }}>
        <NavBC />
        <Routes>
          <Route path="/businessCompany" element={<BusinessCompanyRep />} />
          <Route path="/report" element={<DeliveryReport />} />
          <Route path="/campaignTbl" element={<CampaignTbl />} />
          <Route path="/campaignInfo" element={<CampaignInfo />} />
          <Route path="/productsTbl" element={<ProductTbl />} />
          <Route path="/donationPage" element={<DonationsPage />} />
          <Route path="/productInfo" element={<OneProduct />} />
        </Routes>
      </ThemeContext.Provider>
    </div>
  );
};
