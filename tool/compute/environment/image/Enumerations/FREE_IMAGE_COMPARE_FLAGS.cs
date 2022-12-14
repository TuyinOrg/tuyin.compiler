// ==========================================================
// FreeImage 3 .NET wrapper
// Original FreeImage 3 functions and .NET compatible derived functions
//
// Design and implementation by
// - Jean-Philippe Goerke (jpgoerke@users.sourceforge.net)
// - Carsten Klein (cklein05@users.sourceforge.net)
//
// Contributors:
// - David Boland (davidboland@vodafone.ie)
//
// Main reference : MSDN Knowlede Base
//
// This file is part of FreeImage 3
//
// COVERED CODE IS PROVIDED UNDER THIS LICENSE ON AN "AS IS" BASIS, WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING, WITHOUT LIMITATION, WARRANTIES
// THAT THE COVERED CODE IS FREE OF DEFECTS, MERCHANTABLE, FIT FOR A PARTICULAR PURPOSE
// OR NON-INFRINGING. THE ENTIRE RISK AS TO THE QUALITY AND PERFORMANCE OF THE COVERED
// CODE IS WITH YOU. SHOULD ANY COVERED CODE PROVE DEFECTIVE IN ANY RESPECT, YOU (NOT
// THE INITIAL DEVELOPER OR ANY OTHER CONTRIBUTOR) ASSUME THE COST OF ANY NECESSARY
// SERVICING, REPAIR OR CORRECTION. THIS DISCLAIMER OF WARRANTY CONSTITUTES AN ESSENTIAL
// PART OF THIS LICENSE. NO USE OF ANY COVERED CODE IS AUTHORIZED HEREUNDER EXCEPT UNDER
// THIS DISCLAIMER.
//
// Use at your own risk!
// ==========================================================

// ==========================================================
// CVS
// $Revision: 1.1 $
// $Date: 2007/11/28 15:33:40 $
// $Id: FREE_IMAGE_COMPARE_FLAGS.cs,v 1.1 2007/11/28 15:33:40 cklein05 Exp $
// ==========================================================

namespace compute.environment.imaging
{
	/// <summary>
	/// List of combinable compare modes.
	/// </summary>
	[System.Flags]
	enum FREE_IMAGE_COMPARE_FLAGS
	{
		/// <summary>
		/// Compare headers.
		/// </summary>
		HEADER = 0x1,
		/// <summary>
		/// Compare palettes.
		/// </summary>
		PALETTE = 0x2,
		/// <summary>
		/// Compare pixel data.
		/// </summary>
		DATA = 0x4,
		/// <summary>
		/// Compare meta data.
		/// </summary>
		METADATA = 0x8,
		/// <summary>
		/// Compare everything.
		/// </summary>
		COMPLETE = (HEADER | PALETTE | DATA | METADATA)
	}
}