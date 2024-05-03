import {observer} from "mobx-react-lite";
import {useContext} from "react";
import {Route, Routes} from "react-router-dom";
import {adminRoutes, authRoutes, ownerRoutes, publicRoutes} from "../routes";
import {SHOP_ROUTE, DASHBOARD_ROUTE} from "../utils/consts";
import {Context} from "../index";

const AppRouter = observer(() =>{
   const {user} = useContext(Context);

   return (
     <Routes>
         {user.isAuth && authRoutes.map(({path, Component}) =>
            <Route key={path} path={path} element={<Component/>} exact/>
         )}
         {user.isAdmin() && adminRoutes.map(({path, Component}) =>
             <Route key={path} path={path} element={<Component/>} exact/>
         )}
         {user.isOwner() && ownerRoutes.map(({path, Component}) =>
             <Route key={path} path={DASHBOARD_ROUTE} element={<Component />} exact/>
         )}
         {publicRoutes.map(({path, Component}) =>
            <Route key={path} path={path} element={<Component/>} exact/>
         )}
         <Route path={"*"} element={SHOP_ROUTE} exact/>
     </Routes>
   );
});

export default AppRouter;