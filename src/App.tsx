import React, { useEffect, useState } from 'react';
import "bootstrap/dist/css/bootstrap.min.css"
import { BrowserRouter } from 'react-router-dom';
import  Header from './Layouts/Header/Header';
import  Sidebar from './Layouts/Sidebar/Sidebar';
import  Main from './Layouts/Main/Main';
import { getLoading, setLoading } from './Config/http';

export const AppContext = React.createContext<any>({});
export const baseUrl = "http://localhost:5274/api";

function App() {
  const [profile, setProfile] = useState({
    name: "Johnson",
  });

  const [loading,setLoadingState] = useState(getLoading());

  useEffect(() =>{
    setLoading(loading);
  },[loading])
  
  return (
    <AppContext.Provider value={{ profile, loading }}>
      <BrowserRouter>
        <div className="container-fluid w-100">
          <div className="row">
            <Header />
          </div>
          <div className="row">
            <div className="col-md-3 p-0 h-100">
              <Sidebar />
            </div>
            <div className="col-md-9 mt-3 pl-0">
              <Main />
            </div>
          </div>
        </div>
      </BrowserRouter>
    </AppContext.Provider>

  );
}

export default App;
