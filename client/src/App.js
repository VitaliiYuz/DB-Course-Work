import {observer} from "mobx-react-lite";
import {useContext, useEffect, useState} from "react";
import {BrowserRouter} from "react-router-dom";
import {check} from "./http/userAPI";
import NavBar from "./components/NavBar";
import AppRouter from "./components/AppRouter";
import {Context} from "./index";
import { Loader } from "./components/ui/Loader";


const App = observer(() =>{
  const {user} = useContext(Context);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    check().then(data => {
      user.setUser(data);
      user.setIsAuth(true);
    })
    .catch(err => {
      console.error(err);
    })
    .finally(() => setLoading(false));
  }, []);

  if(loading){
      return <Loader/>
  }

  return (
    <BrowserRouter>
      <NavBar/>
      <AppRouter/>
    </BrowserRouter>
  );

});

export default App;
