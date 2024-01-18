import './App.css';
import {
  BrowserRouter,
  Routes,
  Route,
} from "react-router-dom";
import { Shop } from "./pages/shop/shop";
import { Cart } from "./pages/cart/cart";
import { Login } from "./pages/user/login";
import { Navbar } from "./components/navbar";
import { Signup } from "./pages/user/signup";

import { ShopContextProvider } from './context/shop-context';
import { Setting } from './pages/user/profile/setting';


function App() {
  return (
    <div className="App">
      <ShopContextProvider>
        <BrowserRouter>
          <Navbar>
          </Navbar>
          <Routes>
            <Route path="/" element={<Shop />} />
            <Route path="/cart" element={<Cart />} />
            <Route path="/login" element={<Login />} />
            <Route path="/signup" element={<Signup />} />
            <Route path="/setting" element={<Setting />} />
          </Routes>
        </BrowserRouter>
      </ShopContextProvider>
    </div>
  );

}


export default App;
