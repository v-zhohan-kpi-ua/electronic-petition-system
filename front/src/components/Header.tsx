import Image from "next/image";
import Link from "next/link";

export default function Header() {
  return (
    <header>
      <div className="bg-white py-3 border-b border-gray-200">
        <div className="flex flex-row gap-1.5 max-w-screen-md	mx-auto">
          <Link href="/">
            <div className="relative w-14 sm:w-16 h-16">
              <Image
                src="/images/coat_of_arms.webp"
                alt="Герб Королівства Дикої Природи, Лісів та Парків"
                fill
              />
            </div>
          </Link>
          <div className="text-black">
            <h2 className="text-xl sm:text-2xl font-bold">Петиції</h2>
            <h3 className="text-md sm:text-xl font-medium">
              Уряд Королівства Дикої Природи та Лісів
            </h3>
          </div>
        </div>
      </div>
    </header>
  );
}
