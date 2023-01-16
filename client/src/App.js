import { useAuth0 } from "@auth0/auth0-react";
import { LoginButton } from "./auth0/login.button";
import { Profile } from "./auth0/Profile";
import "./App.css";

//import { HomePageNonProfit } from "./pages/homePgaeOrg/homePage.non_profit.jsx";

export const App = () => {
  //isLoading - מחזיר אמת או שקר אם זה טוען או לא
  //isAuthenticated - מחזיר אמת או שקר אם המשתמש התחבר או לא
  const { isAuthenticated, isLoading } = useAuth0();
  if (!isLoading) {
    return (
      <div className="App">
        {isAuthenticated ? <Profile /> : <LoginButton />}
      </div>
    );
  } else {
    return "Loading";
  }
};
