import {Link} from 'react-router-dom';

export default function Header({
    heading,
    paragraph,
    linkName,
    linkUrl="#"
}){
    return(
        <div className="mb-10">
            <div className="flex justify-center">
                {/* img here  */}
            </div>
            <h2 className="mt-6 text-center text-3xl font-extrabold">
                {heading}
            </h2>
            <p className="mt-2 text-center text-smmt-5">
            {paragraph} {' '}
            <Link to={linkUrl} className="font-medium text-accent hover:text-purple-500">
                {linkName}
            </Link>
            </p>
        </div>
    )
}