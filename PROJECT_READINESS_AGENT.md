# Combination Generator Readiness Agent

## Purpose
This agent is designed to verify that the Combination Generator project meets the homework requirements and is ready for submission.

## Scope
- Angular 20 client application
- Server API implementation
- Required workflows: input n, Start, Next, All permutations with pagination, Reset, Back
- Task compliance and cleanliness for Git submission

## Checkpoints
1. Project structure
   - `CombinationGenerator/` contains Angular 20 application
   - `CombinationGeneratorAPI/` contains server backend
   - `.gitignore` exists at repository root and excludes generated artifacts
2. Input validation
   - `n` accepted between 1 and 20
   - invalid values rejected with user-friendly errors
3. Start API
   - exists at `/api/start`
   - accepts `n`
   - returns total number of permutations
   - stores session state for `current_n` and `current_index`
4. GetNext API
   - exists at `/api/next`
   - returns the next permutation and its serial index
   - does not precompute or return all permutations at once
   - properly returns "אין יותר קומבינציות" when finished
5. GetAll API and pagination
   - exists at `/api/all`
   - accepts page, pageSize, and fromIndex
   - returns only requested page of permutations
   - supports page navigation and jumps
6. Client navigation
   - includes next button
   - includes reset button
   - includes show-all button
   - includes back from all-permutations view to single-view
   - retains the last shown permutation after returning from all-permutations view
7. Architecture
   - backend is modular and organized into separate layers/services
   - frontend is divided into components
   - server-side computations occur on the backend
8. Non-functional and submission requirements
   - no compiled artifacts or `node_modules` are committed
   - repository layout is clean, with only code and configuration files
   - package managers and build config are present

## Scoring rubric
- 0-3: Missing critical functionality or major mismatch
- 4-6: Partial functionality, incomplete pagination or navigation
- 7-8: Most requirements implemented, minor issues or mismatches
- 9-10: Fully meets requirements and is submission-ready

## Instructions for the agent
1. Read the project files under `CombinationGenerator/` and `CombinationGeneratorAPI/`.
2. Confirm API endpoints, request parameters, and responses.
3. Confirm UI flow and routing for input, next, all permutations, and back.
4. Confirm `.gitignore` does not include irrelevant code or artifacts.
5. Compare implemented backend technology with the assignment requirement.
6. Output a checklist and a score.
7. Highlight any gaps or mismatches explicitly.

## Example output format
- Functional completeness: ✅ / ⚠️ / ❌
- Backend language alignment: ✅ / ⚠️ / ❌
- Pagination: ✅ / ⚠️ / ❌
- Multi-user readiness: ✅ / ⚠️ / ❌
- Git cleanliness: ✅ / ⚠️ / ❌
- Score: 7.5 / 10
- Notes: ...
