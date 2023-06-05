import { ImSpinner2 } from "react-icons/im";

export default function LoadingPage() {
  return <div className="h-full flex items-center justify-center">
  <div><ImSpinner2 className="animate-spin-fast mx-auto text-3xl text-gray-500" /></div>
</div>
}