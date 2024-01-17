import './App.css';
import {
  BrowserRouter,
  Routes,
  Route,
} from "react-router-dom";
import {Shop} from "./pages/shop/shop";
import {Cart} from "./pages/cart/cart";
import {Login} from "./pages/user/login";

function App() {
  return (
    <div className="min-h-full h-screen flex items-center justify-center py-12 px-4 sm:px-6 lg:px-8">
    <div className="max-w-md w-full space-y-8">
     <BrowserRouter>
        <Routes>
            <Route path ="/" element={<Shop />}/>
            <Route path ="/cart" element={<Cart />}/>
            <Route path ="/login" element={<Login />}/>
        </Routes>
      </BrowserRouter>
    </div>
  </div>
  );
  
}


export default App;
