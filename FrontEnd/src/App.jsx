import {
  BrowserRouter,
  Routes,
  Route,
} from "react-router-dom";
import SignupPage from './pages/Signup';
import LoginPage from './pages/Login';
import { useState, useEffect } from "react";
import Main from "./pages/Main"
import Info from "./components/Info";

function App() {
  const [loggedState, setLoggedState] = useState(false);


  useEffect(() => {
    const token = localStorage.getItem("auth");
    if (token) {
      setLoggedState(true);
    }
  }, []); 


  return (
    <div className="bg-background min-h-full h-screen flex 
    items-center justify-center py-12 px-4 sm:px-6 lg:px-8">
    <div className="w-screen space-y-8">
     <BrowserRouter>
     <Info loggedState={loggedState}/>
        <Routes>
        <Route
          path="/"
          element={loggedState ? <Main /> : <LoginPage setLoggedState={setLoggedState} />}
        />
        <Route
          path="/login"
          element={loggedState ? <Main /> : <LoginPage setLoggedState={setLoggedState} />}
        />
            <Route path="/signup" element={<SignupPage/>} />
        </Routes>
      </BrowserRouter>
    </div>
  </div>
  );
}

export default App;